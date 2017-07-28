namespace Admin.View
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cmsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.muteTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.muteTsmi1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unmuteTsmi1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kickTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.banTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.tpGlobalChat = new System.Windows.Forms.TabPage();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.illvUsers = new System.Windows.Forms.ImageList(this.components);
            this.rtxMessages = new System.Windows.Forms.RichTextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpSystemPanel = new System.Windows.Forms.TabPage();
            this.lvUsersPanel = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpBanList = new System.Windows.Forms.TabPage();
            this.lvBanList = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsBanList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unbanTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.addIPTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ilTabPageLogo = new System.Windows.Forms.ImageList(this.components);
            this.cmsOptions.SuspendLayout();
            this.tpGlobalChat.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpSystemPanel.SuspendLayout();
            this.tpBanList.SuspendLayout();
            this.cmsBanList.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsOptions
            // 
            this.cmsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muteTsmi,
            this.kickTsmi,
            this.banTsmi});
            this.cmsOptions.Name = "cmsOptions";
            this.cmsOptions.Size = new System.Drawing.Size(103, 70);
            // 
            // muteTsmi
            // 
            this.muteTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muteTsmi1,
            this.unmuteTsmi1});
            this.muteTsmi.Image = global::Admin.Properties.Resources.mute_user;
            this.muteTsmi.Name = "muteTsmi";
            this.muteTsmi.Size = new System.Drawing.Size(102, 22);
            this.muteTsmi.Text = "Mute";
            this.muteTsmi.Click += new System.EventHandler(this.commandOptions);
            // 
            // muteTsmi1
            // 
            this.muteTsmi1.Image = global::Admin.Properties.Resources.mute_user;
            this.muteTsmi1.Name = "muteTsmi1";
            this.muteTsmi1.Size = new System.Drawing.Size(117, 22);
            this.muteTsmi1.Text = "Mute";
            this.muteTsmi1.Click += new System.EventHandler(this.commandOptions);
            // 
            // unmuteTsmi1
            // 
            this.unmuteTsmi1.Image = global::Admin.Properties.Resources.unmute_user;
            this.unmuteTsmi1.Name = "unmuteTsmi1";
            this.unmuteTsmi1.Size = new System.Drawing.Size(117, 22);
            this.unmuteTsmi1.Text = "Unmute";
            this.unmuteTsmi1.Click += new System.EventHandler(this.commandOptions);
            // 
            // kickTsmi
            // 
            this.kickTsmi.Image = global::Admin.Properties.Resources.kick_boot;
            this.kickTsmi.Name = "kickTsmi";
            this.kickTsmi.Size = new System.Drawing.Size(102, 22);
            this.kickTsmi.Text = "Kick";
            this.kickTsmi.Click += new System.EventHandler(this.commandOptions);
            // 
            // banTsmi
            // 
            this.banTsmi.Image = global::Admin.Properties.Resources.ban_user;
            this.banTsmi.Name = "banTsmi";
            this.banTsmi.Size = new System.Drawing.Size(102, 22);
            this.banTsmi.Text = "Ban";
            this.banTsmi.Click += new System.EventHandler(this.commandOptions);
            // 
            // tpGlobalChat
            // 
            this.tpGlobalChat.BackColor = System.Drawing.SystemColors.Control;
            this.tpGlobalChat.Controls.Add(this.lvUsers);
            this.tpGlobalChat.Controls.Add(this.rtxMessages);
            this.tpGlobalChat.Controls.Add(this.btnSendMessage);
            this.tpGlobalChat.Controls.Add(this.txtMessage);
            this.tpGlobalChat.ImageIndex = 0;
            this.tpGlobalChat.Location = new System.Drawing.Point(4, 24);
            this.tpGlobalChat.Name = "tpGlobalChat";
            this.tpGlobalChat.Padding = new System.Windows.Forms.Padding(3);
            this.tpGlobalChat.Size = new System.Drawing.Size(614, 377);
            this.tpGlobalChat.TabIndex = 0;
            this.tpGlobalChat.Text = "Global Chat";
            // 
            // lvUsers
            // 
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.lvUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUsers.Location = new System.Drawing.Point(491, 7);
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(120, 334);
            this.lvUsers.SmallImageList = this.illvUsers;
            this.lvUsers.TabIndex = 5;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvUsers_ColumnWidthChanging);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nickname";
            this.columnHeader6.Width = 116;
            // 
            // illvUsers
            // 
            this.illvUsers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("illvUsers.ImageStream")));
            this.illvUsers.TransparentColor = System.Drawing.Color.Transparent;
            this.illvUsers.Images.SetKeyName(0, "green");
            this.illvUsers.Images.SetKeyName(1, "yellow");
            this.illvUsers.Images.SetKeyName(2, "orange");
            this.illvUsers.Images.SetKeyName(3, "red");
            // 
            // rtxMessages
            // 
            this.rtxMessages.BackColor = System.Drawing.Color.White;
            this.rtxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtxMessages.Location = new System.Drawing.Point(3, 7);
            this.rtxMessages.Name = "rtxMessages";
            this.rtxMessages.ReadOnly = true;
            this.rtxMessages.Size = new System.Drawing.Size(479, 334);
            this.rtxMessages.TabIndex = 4;
            this.rtxMessages.Text = "";
            this.rtxMessages.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxMessages_LinkClicked);
            this.rtxMessages.TextChanged += new System.EventHandler(this.rtxMessages_TextChanged);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSendMessage.Location = new System.Drawing.Point(491, 345);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(120, 29);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMessage.Location = new System.Drawing.Point(3, 345);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(479, 29);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpGlobalChat);
            this.tabMain.Controls.Add(this.tpSystemPanel);
            this.tabMain.Controls.Add(this.tpBanList);
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabMain.ImageList = this.ilTabPageLogo;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(622, 405);
            this.tabMain.TabIndex = 1;
            // 
            // tpSystemPanel
            // 
            this.tpSystemPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tpSystemPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpSystemPanel.Controls.Add(this.lvUsersPanel);
            this.tpSystemPanel.ImageIndex = 1;
            this.tpSystemPanel.Location = new System.Drawing.Point(4, 24);
            this.tpSystemPanel.Name = "tpSystemPanel";
            this.tpSystemPanel.Padding = new System.Windows.Forms.Padding(3);
            this.tpSystemPanel.Size = new System.Drawing.Size(614, 377);
            this.tpSystemPanel.TabIndex = 1;
            this.tpSystemPanel.Text = "System Panel";
            // 
            // lvUsersPanel
            // 
            this.lvUsersPanel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHader4});
            this.lvUsersPanel.ContextMenuStrip = this.cmsOptions;
            this.lvUsersPanel.FullRowSelect = true;
            this.lvUsersPanel.GridLines = true;
            this.lvUsersPanel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUsersPanel.Location = new System.Drawing.Point(0, 0);
            this.lvUsersPanel.Name = "lvUsersPanel";
            this.lvUsersPanel.Size = new System.Drawing.Size(611, 377);
            this.lvUsersPanel.TabIndex = 0;
            this.lvUsersPanel.UseCompatibleStateImageBehavior = false;
            this.lvUsersPanel.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP";
            this.columnHeader1.Width = 143;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nickname";
            this.columnHeader2.Width = 148;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.Width = 112;
            // 
            // columnHader4
            // 
            this.columnHader4.Text = "Last message";
            this.columnHader4.Width = 201;
            // 
            // tpBanList
            // 
            this.tpBanList.Controls.Add(this.lvBanList);
            this.tpBanList.Controls.Add(this.listView1);
            this.tpBanList.ImageIndex = 2;
            this.tpBanList.Location = new System.Drawing.Point(4, 24);
            this.tpBanList.Name = "tpBanList";
            this.tpBanList.Padding = new System.Windows.Forms.Padding(3);
            this.tpBanList.Size = new System.Drawing.Size(614, 377);
            this.tpBanList.TabIndex = 2;
            this.tpBanList.Text = "Ban List";
            this.tpBanList.UseVisualStyleBackColor = true;
            // 
            // lvBanList
            // 
            this.lvBanList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.lvBanList.ContextMenuStrip = this.cmsBanList;
            this.lvBanList.FullRowSelect = true;
            this.lvBanList.GridLines = true;
            this.lvBanList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBanList.Location = new System.Drawing.Point(0, 0);
            this.lvBanList.Name = "lvBanList";
            this.lvBanList.Size = new System.Drawing.Size(611, 374);
            this.lvBanList.TabIndex = 1;
            this.lvBanList.UseCompatibleStateImageBehavior = false;
            this.lvBanList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "IP Address";
            this.columnHeader4.Width = 177;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "The last time tried to connect";
            this.columnHeader5.Width = 190;
            // 
            // cmsBanList
            // 
            this.cmsBanList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unbanTsmi,
            this.addIPTsmi});
            this.cmsBanList.Name = "cmsBanList";
            this.cmsBanList.Size = new System.Drawing.Size(110, 48);
            // 
            // unbanTsmi
            // 
            this.unbanTsmi.Image = global::Admin.Properties.Resources.unban_user;
            this.unbanTsmi.Name = "unbanTsmi";
            this.unbanTsmi.Size = new System.Drawing.Size(109, 22);
            this.unbanTsmi.Text = "Unban";
            this.unbanTsmi.Click += new System.EventHandler(this.addIPTsmi_Click);
            // 
            // addIPTsmi
            // 
            this.addIPTsmi.Image = global::Admin.Properties.Resources.ban_user;
            this.addIPTsmi.Name = "addIPTsmi";
            this.addIPTsmi.Size = new System.Drawing.Size(109, 22);
            this.addIPTsmi.Text = "Add IP";
            this.addIPTsmi.Click += new System.EventHandler(this.addIPTsmi_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(608, 371);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // ilTabPageLogo
            // 
            this.ilTabPageLogo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabPageLogo.ImageStream")));
            this.ilTabPageLogo.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabPageLogo.Images.SetKeyName(0, "Global Chat.png");
            this.ilTabPageLogo.Images.SetKeyName(1, "System Panel.png");
            this.ilTabPageLogo.Images.SetKeyName(2, "Ban List.png");
            this.ilTabPageLogo.Images.SetKeyName(3, "Private Chat.png");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 403);
            this.Controls.Add(this.tabMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.cmsOptions.ResumeLayout(false);
            this.tpGlobalChat.ResumeLayout(false);
            this.tpGlobalChat.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tpSystemPanel.ResumeLayout(false);
            this.tpBanList.ResumeLayout(false);
            this.cmsBanList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsOptions;
        private System.Windows.Forms.ToolStripMenuItem muteTsmi;
        private System.Windows.Forms.ToolStripMenuItem muteTsmi1;
        private System.Windows.Forms.ToolStripMenuItem unmuteTsmi1;
        private System.Windows.Forms.ToolStripMenuItem kickTsmi;
        private System.Windows.Forms.ToolStripMenuItem banTsmi;
        private System.Windows.Forms.TabPage tpSystemPanel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tpGlobalChat;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TabControl tabMain;
        public System.Windows.Forms.ListView lvUsersPanel;
        public System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ColumnHeader columnHader4;
        public System.Windows.Forms.RichTextBox rtxMessages;
        private System.Windows.Forms.TabPage tpBanList;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip cmsBanList;
        private System.Windows.Forms.ToolStripMenuItem unbanTsmi;
        public System.Windows.Forms.ListView lvBanList;
        private System.Windows.Forms.ToolStripMenuItem addIPTsmi;
        private System.Windows.Forms.ImageList ilTabPageLogo;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ImageList illvUsers;
        public System.Windows.Forms.ListView lvUsers;
    }
}

