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
            this.RoomList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.SendChatButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.skype = new AxSKYPE4COMLib.AxSkype();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.クライアントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttachSkypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatPanel = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skype)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomList
            // 
            this.RoomList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoomList.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RoomList.FormattingEnabled = true;
            this.RoomList.HorizontalScrollbar = true;
            this.RoomList.ItemHeight = 15;
            this.RoomList.Location = new System.Drawing.Point(524, 35);
            this.RoomList.Name = "RoomList";
            this.RoomList.Size = new System.Drawing.Size(192, 499);
            this.RoomList.TabIndex = 1;
            this.RoomList.SelectedIndexChanged += new System.EventHandler(this.RoomList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(548, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ルーム一覧";
            // 
            // ChatBox
            // 
            this.ChatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatBox.Location = new System.Drawing.Point(5, 474);
            this.ChatBox.Multiline = true;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ChatBox.Size = new System.Drawing.Size(513, 129);
            this.ChatBox.TabIndex = 5;
            // 
            // SendChatButton
            // 
            this.SendChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendChatButton.Location = new System.Drawing.Point(548, 574);
            this.SendChatButton.Name = "SendChatButton";
            this.SendChatButton.Size = new System.Drawing.Size(167, 29);
            this.SendChatButton.TabIndex = 6;
            this.SendChatButton.Text = "チャット送信";
            this.SendChatButton.UseVisualStyleBackColor = true;
            this.SendChatButton.Click += new System.EventHandler(this.SendChatButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(719, 23);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(140, 18);
            this.toolStripStatusLabel.Text = "ここにステータスを表示";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TIM Skype Chat Client";
            this.notifyIcon.Visible = true;
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.Checked = true;
            this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(550, 552);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(97, 16);
            this.autoScrollCheckBox.TabIndex = 16;
            this.autoScrollCheckBox.Text = "オートスクロール";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // skype
            // 
            this.skype.Enabled = true;
            this.skype.Location = new System.Drawing.Point(716, 598);
            this.skype.Name = "skype";
            this.skype.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("skype.OcxState")));
            this.skype.Size = new System.Drawing.Size(192, 192);
            this.skype.TabIndex = 15;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.クライアントToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(719, 26);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // クライアントToolStripMenuItem
            // 
            this.クライアントToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachSkypeToolStripMenuItem,
            this.ReloadMessagesToolStripMenuItem,
            this.ExitClientToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.クライアントToolStripMenuItem.Name = "クライアントToolStripMenuItem";
            this.クライアントToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.クライアントToolStripMenuItem.Text = "クライアント";
            // 
            // AttachSkypeToolStripMenuItem
            // 
            this.AttachSkypeToolStripMenuItem.Name = "AttachSkypeToolStripMenuItem";
            this.AttachSkypeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.AttachSkypeToolStripMenuItem.Text = "Skype連携を行う";
            this.AttachSkypeToolStripMenuItem.Click += new System.EventHandler(this.AttachSkypeToolStripMenuItem_Click);
            // 
            // ReloadMessagesToolStripMenuItem
            // 
            this.ReloadMessagesToolStripMenuItem.Name = "ReloadMessagesToolStripMenuItem";
            this.ReloadMessagesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ReloadMessagesToolStripMenuItem.Text = "メッセージ再読み込み";
            this.ReloadMessagesToolStripMenuItem.Click += new System.EventHandler(this.ReloadMessagesToolStripMenuItem_Click);
            // 
            // ExitClientToolStripMenuItem
            // 
            this.ExitClientToolStripMenuItem.Name = "ExitClientToolStripMenuItem";
            this.ExitClientToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ExitClientToolStripMenuItem.Text = "クライアントを終了する";
            this.ExitClientToolStripMenuItem.Click += new System.EventHandler(this.ExitClientToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // chatPanel
            // 
            this.chatPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatPanel.Location = new System.Drawing.Point(5, 35);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(513, 433);
            this.chatPanel.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 639);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.autoScrollCheckBox);
            this.Controls.Add(this.skype);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SendChatButton);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RoomList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Skype Chat Client";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skype)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox RoomList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ChatBox;
        private System.Windows.Forms.Button SendChatButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private AxSKYPE4COMLib.AxSkype skype;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox autoScrollCheckBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem クライアントToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AttachSkypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReloadMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitClientToolStripMenuItem;
        private System.Windows.Forms.Panel chatPanel;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
    }
}

