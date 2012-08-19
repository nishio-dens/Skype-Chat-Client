using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxSKYPE4COMLib;
using Growl.Connector;
using SKYPE4COMLib;

namespace SkypeChatClient
{
    public partial class MainForm : Form
    {
        //Key = Blob名, Value = ウィンドウ
        IDictionary<string, RichTextBox> ChatWindow { get; set; }
        IList<ChatRoomInformation> Rooms { get; set; }

        ChatClient Client { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.MainForm_Closed);

            ChatWindow = new Dictionary<string, RichTextBox>();
            Rooms = new List<ChatRoomInformation>();

            ChatBox.KeyUp += (obj, e) =>
            {
                if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.Shift)
                {
                    //最後が改行の時だけ反応させる
                    if (ChatBox.Text.EndsWith("\n"))
                    {
                        SendMessageFromTextBox();
                    }
                }
            };
        }

        void AttachSkypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttachSkype();
        }

        void AttachSkype()
        {
            Client = new ChatClient(skype);
            Client.AttachToSkypeClient();
            Client.AddMessageReceivedListener(new Baloon(notifyIcon));
            Client.AddMessageReceivedListener(new ChatRenderer(this));

            ReloadDisplayMessages();
        }

        void ReloadDisplayMessages()
        {
            SetNotificationMessage("メッセージの更新を行います。");
            Client.ReloadAllMessages();
            ReloadRooms();
            ReloadMessages();
        }

        private void ReloadMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadDisplayMessages();
        }

        void ReloadRooms()
        {
            var rooms = Client.GetChatRooms()
                .OrderByDescending(t => t.ActivityTimestamp);

            foreach (var room in rooms)
            {
                Rooms.Add(new ChatRoomInformation
                {
                    Blob = room.Blob,
                    FriendlyRoomName = room.FriendlyName
                });
            }
            ReloadRoomsList();
        }

        void ReloadRoomsList()
        {
            RoomList.Items.Clear();
            foreach (var room in Rooms)
            {
                RoomList.Items.Add(room.FriendlyRoomName);
            }
        }

        void ReloadMessages()
        {
            ChatWindow.Clear();
            var blobs = Client.GetBlobs();
            foreach (var blob in blobs)
            {
                CreateNewChatTextWindow(blob);
            }

            var messages = Client.ReceivedMessages
                .OrderBy(i => i.Timestamp);

            foreach (var message in messages)
            {
                AppendDecoratedChatMessage(ChatWindow[message.Chat.Blob], message);
            }
            SetCurrentChatRoom(Rooms.First().Blob);
        }

        void CreateNewChatTextWindow(string blobName)
        {
            if (ChatWindow.ContainsKey(blobName))
            {
                return;
            }

            var richTextBox = new RichTextBox();
            richTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            richTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox.Width = chatPanel.Width;
            richTextBox.Height = chatPanel.Height;
            richTextBox.Text = "";
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
            ChatWindow.Add(blobName, richTextBox);
        }

        /// <summary>
        /// 新しく受信したメッセージをチャットウィンドウに表示します。
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="message"></param>
        public void AppendDecoratedChatMessage(string blobName, IChatMessage message)
        {
            if (!ChatWindow.ContainsKey(blobName))
            {
                CreateNewChatTextWindow(blobName);
                ReloadRooms();
            }
            AppendDecoratedChatMessage(ChatWindow[blobName], message);
        }

        void AppendDecoratedChatMessage(RichTextBox richTextBox, IChatMessage message)
        {
            richTextBox.SelectionColor = Color.FromArgb(150, 150, 150);
            richTextBox.AppendText(
                String.Format("{0:00}/{1:00}/{2:00}:{3:00} ",
                    message.Timestamp.Month,
                    message.Timestamp.Day,
                    message.Timestamp.Hour,
                    message.Timestamp.Minute,
                    message.FromHandle,
                    message.Body));

            richTextBox.SelectionColor = Color.FromArgb(65, 105, 225);
            var name = String.Empty;
            if (message.FromDisplayName != null)
            {
                name = message.FromDisplayName;
            }
            else
            {
                name = message.FromHandle;
            }
            richTextBox.AppendText(String.Format("({0}) ", name));

            richTextBox.SelectionColor = Color.Black;
            richTextBox.AppendText(String.Format("{0}\r\n", message.Body));
        }

        void SetCurrentChatRoom(string blobName)
        {
            chatPanel.Controls.Clear();
            chatPanel.Controls.Add(ChatWindow[blobName]);
        }

        private void SetNotificationMessage(string text)
        {
            toolStripStatusLabel.Text = text;
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

        private void ExitClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// メッセージボックスに入力したメッセージを送信します。
        /// </summary>
        void SendMessageFromTextBox()
        {
            var text = ChatBox.Text;
            try
            {
                var textNoSpace = text.ToString();
                if (textNoSpace.Replace('\n', ' ').Replace('\r', ' ').Replace(" ", "").Length > 0)
                {
                    var blob = GetCurrentRoomBlob();
                    var message = Client.SendMessage(blob, text);
                    ChatBox.Clear();
                    AppendDecoratedChatMessage(blob, message);
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

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var conn = new GrowlConnector();
            conn.Register(new Growl.Connector.Application("SkypeChatClient"), new NotificationType[] { new NotificationType("SkypeChatClient", "SkypeChatClient") });
            conn.Notify(new Notification("SkypeChatClient", "SkypeChatClient", DateTime.Now.ToString(), "Hello", "World"));
        }

        string GetCurrentRoomBlob()
        {
            return Rooms.ElementAt(RoomList.SelectedIndex).Blob;
        }

        void RoomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentChatRoom(GetCurrentRoomBlob());
        }
    }
}
