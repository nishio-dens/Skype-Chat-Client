namespace SkypeChatClient
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.UserList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chatMessageBox = new System.Windows.Forms.RichTextBox();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.SendChatButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.senderNameLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ChatTabControl = new System.Windows.Forms.TabControl();
            this.label4 = new System.Windows.Forms.Label();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadButton = new System.Windows.Forms.Button();
            this.skype = new AxSKYPE4COMLib.AxSkype();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skype)).BeginInit();
            this.SuspendLayout();
            // 
            // UserList
            // 
            this.UserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 12;
            this.UserList.Location = new System.Drawing.Point(544, 23);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(166, 412);
            this.UserList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(542, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "参加者一覧";
            // 
            // chatMessageBox
            // 
            this.chatMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatMessageBox.Location = new System.Drawing.Point(131, 472);
            this.chatMessageBox.Name = "chatMessageBox";
            this.chatMessageBox.Size = new System.Drawing.Size(409, 67);
            this.chatMessageBox.TabIndex = 4;
            this.chatMessageBox.Text = "";
            this.chatMessageBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ChatMessageBox_LinkClicked);
            // 
            // ChatBox
            // 
            this.ChatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatBox.Location = new System.Drawing.Point(5, 545);
            this.ChatBox.Multiline = true;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ChatBox.Size = new System.Drawing.Size(535, 58);
            this.ChatBox.TabIndex = 5;
            // 
            // SendChatButton
            // 
            this.SendChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendChatButton.Location = new System.Drawing.Point(542, 574);
            this.SendChatButton.Name = "SendChatButton";
            this.SendChatButton.Size = new System.Drawing.Size(167, 29);
            this.SendChatButton.TabIndex = 6;
            this.SendChatButton.Text = "チャット送信";
            this.SendChatButton.UseVisualStyleBackColor = true;
            this.SendChatButton.Click += new System.EventHandler(this.SendChatButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "発言者";
            // 
            // senderNameLabel
            // 
            this.senderNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.senderNameLabel.AutoSize = true;
            this.senderNameLabel.Location = new System.Drawing.Point(6, 494);
            this.senderNameLabel.Name = "senderNameLabel";
            this.senderNameLabel.Size = new System.Drawing.Size(119, 12);
            this.senderNameLabel.TabIndex = 8;
            this.senderNameLabel.Text = "発言をクリックしてください";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 23);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(140, 18);
            this.toolStripStatusLabel.Text = "ここにステータスを表示";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(546, 474);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(166, 31);
            this.ConnectButton.TabIndex = 10;
            this.ConnectButton.Text = "Skype連携開始";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ChatTabControl
            // 
            this.ChatTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatTabControl.Location = new System.Drawing.Point(1, 1);
            this.ChatTabControl.Name = "ChatTabControl";
            this.ChatTabControl.SelectedIndex = 0;
            this.ChatTabControl.Size = new System.Drawing.Size(539, 435);
            this.ChatTabControl.TabIndex = 0;
            this.ChatTabControl.SelectedIndexChanged += new System.EventHandler(this.ChatTabControl_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "フィルター";
            // 
            // filterTextBox
            // 
            this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTextBox.Location = new System.Drawing.Point(67, 444);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(473, 19);
            this.filterTextBox.TabIndex = 12;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyFilterButton.Location = new System.Drawing.Point(546, 441);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(78, 28);
            this.applyFilterButton.TabIndex = 13;
            this.applyFilterButton.Text = "フィルタ適用";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            this.applyFilterButton.Click += new System.EventHandler(this.FilterApplyButton_Click);
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilterButton.Location = new System.Drawing.Point(630, 441);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(78, 28);
            this.clearFilterButton.TabIndex = 14;
            this.clearFilterButton.Text = "フィルタクリア";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            this.clearFilterButton.Click += new System.EventHandler(this.clearFilterButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TIM Skype Chat Client";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.Checked = true;
            this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(544, 552);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(97, 16);
            this.autoScrollCheckBox.TabIndex = 16;
            this.autoScrollCheckBox.Text = "オートスクロール";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // reloadButton
            // 
            this.reloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadButton.Enabled = false;
            this.reloadButton.Location = new System.Drawing.Point(546, 510);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(166, 31);
            this.reloadButton.TabIndex = 17;
            this.reloadButton.Text = "メッセージ再読み込み";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // skype
            // 
            this.skype.Enabled = true;
            this.skype.Location = new System.Drawing.Point(677, 547);
            this.skype.Name = "skype";
            this.skype.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("skype.OcxState")));
            this.skype.Size = new System.Drawing.Size(192, 192);
            this.skype.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 639);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.autoScrollCheckBox);
            this.Controls.Add(this.skype);
            this.Controls.Add(this.clearFilterButton);
            this.Controls.Add(this.applyFilterButton);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.senderNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SendChatButton);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.chatMessageBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.ChatTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Skype Chat Client(仮)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skype)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox chatMessageBox;
        private System.Windows.Forms.TextBox ChatBox;
        private System.Windows.Forms.Button SendChatButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label senderNameLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TabControl ChatTabControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Button clearFilterButton;
        private AxSKYPE4COMLib.AxSkype skype;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox autoScrollCheckBox;
        private System.Windows.Forms.Button reloadButton;
    }
}

