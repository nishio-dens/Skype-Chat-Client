﻿using System;
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
        //チャットリストウィンドウ。Key = Blob名, Value = ウィンドウ
        IDictionary<string, ListBox> ChatListWindow { get; set; }
        IDictionary<string, TabPage> ChatTabs { get; set; }
        //タブの順番通りのBlob
        IList<string> BlobList { get; set; }
        //現在選択しているタブ
        int SelectedTabIndex { get; set; }

        ChatClient Client { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.MainForm_Closed);
            Client = new ChatClient(skype);
            Client.AttachToSkypeClient();

            ChatListWindow = new Dictionary<string, ListBox>();
            ChatTabs = new Dictionary<string, TabPage>();
            BlobList = new List<string>();
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

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Init()
        {
            AddMessageStatusHandler(skype);
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
                    Client.SendMessage(blob, text);
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
