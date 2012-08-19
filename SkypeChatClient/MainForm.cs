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
using System.Threading;
using System.Threading.Tasks;

namespace SkypeChatClient
{
    public partial class MainForm : Form
    {
        //チャットリストウィンドウ。Key = Blob名, Value = ウィンドウ
        IDictionary<string, ListBox> ChatListWindow { get; set; }
        IDictionary<string, RichTextBox> ChatWindow { get; set; }
        //タブの順番通りのBlob
        IList<string> BlobList { get; set; }
        //現在選択しているタブ
        int SelectedTabIndex { get; set; }

        ChatClient Client { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.MainForm_Closed);

            ChatListWindow = new Dictionary<string, ListBox>();
            ChatWindow = new Dictionary<string, RichTextBox>();
            BlobList = new List<string>();
            SelectedTabIndex = 0;

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
            //Client = new ChatClient(skype);
            //Client.AttachToSkypeClient();
            //Client.AddMessageReceivedListener(new Baloon(notifyIcon));

            SetNotificationMessage("メッセージの更新を行います。");
            //Client.ReloadAllMessages();
            //ReloadRooms();
            //ReloadMessages();


        }

        void ReloadRooms()
        {
            var rooms = Client.GetChatRooms()
                .OrderByDescending(t => t.Timestamp)
                .Select(i => i.FriendlyName);

            RoomList.Items.Clear();
            foreach (var room in rooms)
            {
                RoomList.Items.Add(room);
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
                ChatWindow[message.Chat.Blob].AppendText(message.Body);
            }
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
                    var blob = BlobList[SelectedTabIndex];
                    Client.SendMessage(blob, text);
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

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chatPanel.Controls.Clear();
            var rich = new RichTextBox();
            rich.Text = "bunbunbun";
            chatPanel.Controls.Add(rich);
        }
    }
}
