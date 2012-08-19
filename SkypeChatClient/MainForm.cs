using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKYPE4COMLib;
using AxSKYPE4COMLib;

namespace SkypeChatClient
{
    // TODO: クラスの分離, 副作用のある関数を減らす, リファクタリング
    public partial class MainForm : Form
    {
        //チャット及びチャットグループ一覧
        IList<Chat> Chats { get; set; }
        //メッセージ一覧。Key = Blob名, Value = メッセージ一覧
        IDictionary<string, IList<IChatMessage>> Messages { get; set; }
        //チャットリストウィンドウ。Key = Blob名, Value = ウィンドウ
        IDictionary<string, ListBox> ChatListWindow { get; set; }
        IDictionary<string, TabPage> ChatTabs { get; set; }
        //タブの順番通りのBlob
        IList<string> BlobList { get; set; }
        //現在選択しているタブ
        int SelectedTabIndex { get; set; }
        //最近受信したチャットのblob
        string RecentReceivedChatBlob { get; set; }
        //既に購読したメッセージ
        IList<int> AlreadyReceivedMessage { get; set; }

        ChatClient Client { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.MainForm_Closed);
            Client = new ChatClient();

            Messages = new Dictionary<string, IList<IChatMessage>>();
            ChatListWindow = new Dictionary<string, ListBox>();
            ChatTabs = new Dictionary<string, TabPage>();
            Chats = new List<Chat>();
            BlobList = new List<string>();
            AlreadyReceivedMessage = new List<int>();
            SelectedTabIndex = 0;

            ChatBox.KeyUp += (obj, e) =>
                {
                    if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.Shift)
                    {
                        //最後が改行の時だけ反応させる
                        if (ChatBox.Text.EndsWith("\n"))
                        {
                            //メッセージ送信
                            SendMessageFromTextBox();
                        }
                    }
                };
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Init();
        }

        string GetChatFormattedMessage(IChatMessage message)
        {
            return String.Format("{0:00}/{1:00}/{2:00}:{3:00} {4, -15}: {5}",
                        message.Timestamp.Month,
                        message.Timestamp.Day,
                        message.Timestamp.Hour,
                        message.Timestamp.Minute,
                        message.FromHandle,
                        message.Body);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Init()
        {
            if (skype.Client.IsRunning == false)
            {
                if (MessageBox.Show("Skypeを起動してもよろしいですか？", "Client", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    skype.Client.Start();
                }
                else
                {
                    return;
                }
            }
            try
            {
                //skype初期化
                skype.Attach();
            }
            catch
            {
                MessageBox.Show("Skypeと連携できませんでした。本プログラムからSkypeにアクセスできるよう許可してください。", "Error");
                return;
            }
            ReloadAllMessages();
            //オンラインユーザ一覧
            foreach (Group g in skype.Groups)
            {
                UserList.Items.Clear();
                var userList = new List<string>();
                foreach (User user in g.OnlineUsers)
                {
                    userList.Add(user.Handle);
                }
                var users = userList.Distinct().OrderBy(p => p.ToString());
                foreach (var u in users)
                {
                    UserList.Items.Add(u);
                }
            }
            //メッセージ通知追加
            AddNewMessageToList(skype);
            AddMessageStatusHandler(skype);
        }

        /// <summary>
        /// 新規メッセージが到着したら、メッセージリストに追加します。
        /// </summary>
        /// <param name="skype"></param>
        void AddNewMessageToList(AxSkype skype)
        {
            skype.MessageStatus += new AxSKYPE4COMLib._ISkypeEvents_MessageStatusEventHandler((a, b) =>
            {
                ChatMessageClass chat = b.pMessage as ChatMessageClass;
                //既に存在するメッセージは無視
                if (AlreadyReceivedMessage.Where(i => i == chat.Id).Count() >= 1)
                {
                    return;
                }
                AlreadyReceivedMessage.Add(chat.Id);
                var list = GetChatListWindow(chat.Chat.Blob, chat.Chat.FriendlyName);
                list.Items.Add(GetChatFormattedMessage(chat));
                //スクロールバーを一番下にする
                if (autoScrollCheckBox.CheckState == CheckState.Checked)
                {
                    list.SelectedIndex = list.Items.Count - 1;
                }
                //メッセージを保存しておきます
                if (!Messages.ContainsKey(chat.Chat.Blob))
                {
                    Messages.Add(chat.Chat.Blob, new List<IChatMessage>());
                }
                Messages[chat.Chat.Blob].Add(chat);
            });
        }

        /// <summary>
        /// チャットメッセージを表示するリストを返します。存在しない場合は新規でリストを作ります。
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="friendlyName"></param>
        /// <returns></returns>
        ListBox GetChatListWindow(string blob, string friendlyName)
        {
            if (ChatListWindow.ContainsKey(blob))
            {
                return ChatListWindow[blob];
            }
            ListBox list;
            TabPage tab;
            AddNewChatList(blob, friendlyName, out list, out tab);
            ChatListWindow.Add(blob, list);
            ChatTabs[blob] = tab;
            return list;
        }

        /// <summary>
        /// チャットをバルーンとして表示させるようにします。
        /// </summary>
        /// <param name="skype"></param>
        void AddMessageStatusHandler(AxSkype skype)
        {
            skype.MessageStatus += new AxSKYPE4COMLib._ISkypeEvents_MessageStatusEventHandler((a, b) =>
            {
                ChatMessageClass chat = b.pMessage as ChatMessageClass;
                if (chat.Status == TChatMessageStatus.cmsReceived)
                {
                    notifyIcon.BalloonTipTitle = chat.Sender.Handle + "(" + chat.Chat.FriendlyName + ")";
                    notifyIcon.BalloonTipText = chat.Body;
                    notifyIcon.ShowBalloonTip(1000);
                    // 最近受信したチャットのブロブ
                    RecentReceivedChatBlob = chat.Chat.Blob;
                }
            });
        }

        /// <summary>
        /// チャット内のメッセージを、GUIラベルに表示させます。
        /// </summary>
        /// <param name="text"></param>
        void SetChatMessage(string text)
        {
            var m = text.Substring(12);
            var nameAndMessage = m.Split(new char[] { ':' }, 2);
            senderNameLabel.Text = nameAndMessage[0];
            chatMessageBox.Text = nameAndMessage[1].TrimStart(' ');
        }

        /// <summary>
        /// 新しいチャットウィンドウタブを作成します。
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="friendlyName"></param>
        /// <param name="list">チャットを表示するリストを作成し、取得します。</param>
        /// <param name="tab">チャットを表示するタブを作成し、取得します。</param>
        void AddNewChatList(string blob, string friendlyName, out ListBox list, out TabPage tab)
        {
            var chatPage = new TabPage();
            var chatList = new ListBox();
            chatList.Dock = DockStyle.Fill;
            chatList.Font = new Font("ＭＳ ゴシック", 9);
            chatList.HorizontalScrollbar = true;
            chatList.Click += (obj, eventHandler) =>
            {
                var box = obj as ListBox;
                try
                {
                    SetChatMessage(box.SelectedItem.ToString());
                }
                catch
                { }
            };
            chatList.KeyUp += (obj, eventHandler) =>
            {
                var box = obj as ListBox;
                try
                {
                    SetChatMessage(box.SelectedItem.ToString());
                }
                catch
                { }
            };

            if (friendlyName.Length > 0)
            {
                chatPage.Text = friendlyName;
            }
            else
            {
                chatPage.Text = "NoTitle";
            }
            chatPage.Controls.Add(chatList);
            ChatTabControl.Controls.Add(chatPage);

            tab = chatPage;
            list = chatList;
            BlobList.Add(blob);
        }

        /// <summary>
        /// チャット内のメッセージ一覧を取得します。
        /// </summary>
        /// <param name="chat"></param>
        IList<IChatMessage> GetMessageFromChat(Chat chat)
        {
            var messages = new List<IChatMessage>();
            foreach (IChatMessage message in chat.Messages)
            {
                messages.Add(message);
            }
            return messages;
        }

        /// <summary>
        /// すべてのメッセージを再読み込みし、チャットリストに追加します。
        /// </summary>
        void ReloadAllMessages()
        {
            //チャット及びチャットグループ一覧の追加
            Chats.Clear();
            foreach (Chat chat in skype.Chats)
            {
                Chats.Add(chat);
            }
            //チャット内からメッセージ一覧を取得
            foreach (var chat in Chats)
            {
                Messages[chat.Blob] = new List<IChatMessage>();
                foreach (var item in GetMessageFromChat(chat))
                {
                    Messages[chat.Blob].Add(item);
                }
            }
            //既にチャットリストが存在していたら、いったん中身を削除
            foreach (var chatList in ChatListWindow.Values)
            {
                chatList.Items.Clear();
            }
            //取得したメッセージをチャットリストに追加
            foreach (var blob in Messages.Keys)
            {
                var messages = Messages[blob].OrderBy(m => m.Timestamp);
                foreach (var m in messages)
                {
                    var list = GetChatListWindow(m.Chat.Blob, m.Chat.FriendlyName);
                    list.Items.Add(GetChatFormattedMessage(m));
                }
            }
        }

        private void FilterApplyButton_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void clearFilterButton_Click(object sender, EventArgs e)
        {
            ClearFilter();
        }

        void ApplyFilter()
        {
            //フィルタを適用します。
            var blob = BlobList[SelectedTabIndex];
            var searchWord = filterTextBox.Text;
            var messages = Messages[blob]
                .Where(c => c.Body.Contains(searchWord) || c.FromHandle.Contains(searchWord) || c.Timestamp.ToString().Contains(searchWord))
                .OrderBy(c => c.Timestamp);
            var list = ChatListWindow[blob];
            list.Items.Clear();
            foreach (var m in messages)
            {
                list.Items.Add(GetChatFormattedMessage(m));
            }
        }

        void ClearFilter()
        {
            //フィルタ適用を解除します。
            var blob = BlobList[SelectedTabIndex];
            var messages = Messages[blob].OrderBy(c => c.Timestamp);
            var list = ChatListWindow[blob];
            list.Items.Clear();
            foreach (var m in messages)
            {
                list.Items.Add(GetChatFormattedMessage(m));
            }
        }


        private void ChatTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var tab = sender as TabControl;
                SelectedTabIndex = tab.SelectedIndex;
                if (tab != null)
                {
                    var blob = BlobList[SelectedTabIndex];
                    SetNotificationMessage("Selected Tab: " + Messages[blob].First().Chat.FriendlyName);
                    var messages = Messages[blob];
                }
            }
            catch
            {
            }
        }

        private void SetNotificationMessage(string text)
        {
            toolStripStatusLabel.Text = text;
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            ReloadAllMessages();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        void MainForm_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("終了してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// メッセージボックスに入力したメッセージを送信します。
        /// </summary>
        private void SendMessageFromTextBox()
        {
            var text = ChatBox.Text;
            try
            {
                var textNoSpace = text.ToString();
                if (textNoSpace.Replace('\n', ' ').Replace('\r', ' ').Replace(" ", "").Length > 0)
                {
                    var blob = BlobList[SelectedTabIndex];
                    var firstMessage = Messages[blob].First();
                    ChatClient.SendMessage(firstMessage.Chat, text);
                    //チャットを消す
                    ChatBox.Clear();
                }
            }
            catch
            {
                MessageBox.Show("メッセージ　" + text + "　は送信できませんでした");
            }
        }

        private void SendChatButton_Click(object sender, EventArgs e)
        {
            SendMessageFromTextBox();
        }

        private void ChatMessageBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //URIがクリックされたら、ブラウザを起動
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            // 受信したチャットメッセージを表示するタブを設定
            try
            {
                ChatTabControl.SelectedTab = ChatTabs[RecentReceivedChatBlob];
                // メインウィンドウを表示
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            catch
            {
                MessageBox.Show("タブが存在しませんでした。");
            }
        }

    }
}
