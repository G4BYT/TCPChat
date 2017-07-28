namespace Client.View
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpGlobalChat = new System.Windows.Forms.TabPage();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.privateChatTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.blockTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.unblockTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.illvUsers = new System.Windows.Forms.ImageList(this.components);
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.rtxMessages = new System.Windows.Forms.RichTextBox();
            this.ilTabPageLogo = new System.Windows.Forms.ImageList(this.components);
            this.tabMain.SuspendLayout();
            this.tpGlobalChat.SuspendLayout();
            this.cmsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpGlobalChat);
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabMain.ImageList = this.ilTabPageLogo;
            this.tabMain.Location = new System.Drawing.Point(2, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(605, 413);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tpGlobalChat
            // 
            this.tpGlobalChat.BackColor = System.Drawing.SystemColors.Control;
            this.tpGlobalChat.Controls.Add(this.lvUsers);
            this.tpGlobalChat.Controls.Add(this.btnSend);
            this.tpGlobalChat.Controls.Add(this.txtMessage);
            this.tpGlobalChat.Controls.Add(this.rtxMessages);
            this.tpGlobalChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tpGlobalChat.ImageIndex = 0;
            this.tpGlobalChat.Location = new System.Drawing.Point(4, 24);
            this.tpGlobalChat.Name = "tpGlobalChat";
            this.tpGlobalChat.Padding = new System.Windows.Forms.Padding(3);
            this.tpGlobalChat.Size = new System.Drawing.Size(597, 385);
            this.tpGlobalChat.TabIndex = 0;
            this.tpGlobalChat.Text = "Global Chat";
            // 
            // lvUsers
            // 
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvUsers.ContextMenuStrip = this.cmsOptions;
            this.lvUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUsers.Location = new System.Drawing.Point(477, 0);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(120, 348);
            this.lvUsers.SmallImageList = this.illvUsers;
            this.lvUsers.TabIndex = 4;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvUsers_ColumnWidthChanging);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nickname";
            this.columnHeader1.Width = 116;
            // 
            // cmsOptions
            // 
            this.cmsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.privateChatTsmi,
            this.blockTsmi,
            this.unblockTsmi});
            this.cmsOptions.Name = "cmsOptions";
            this.cmsOptions.Size = new System.Drawing.Size(139, 70);
            // 
            // privateChatTsmi
            // 
            this.privateChatTsmi.Image = global::Client.Properties.Resources.privatechat;
            this.privateChatTsmi.Name = "privateChatTsmi";
            this.privateChatTsmi.Size = new System.Drawing.Size(138, 22);
            this.privateChatTsmi.Text = "Private Chat";
            this.privateChatTsmi.Click += new System.EventHandler(this.cmsOptions_Click);
            // 
            // blockTsmi
            // 
            this.blockTsmi.Image = global::Client.Properties.Resources.ban_user;
            this.blockTsmi.Name = "blockTsmi";
            this.blockTsmi.Size = new System.Drawing.Size(138, 22);
            this.blockTsmi.Text = "Block";
            this.blockTsmi.Click += new System.EventHandler(this.cmsOptions_Click);
            // 
            // unblockTsmi
            // 
            this.unblockTsmi.Image = global::Client.Properties.Resources.unban_user;
            this.unblockTsmi.Name = "unblockTsmi";
            this.unblockTsmi.Size = new System.Drawing.Size(138, 22);
            this.unblockTsmi.Text = "Unblock";
            this.unblockTsmi.Click += new System.EventHandler(this.cmsOptions_Click);
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
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSend.Location = new System.Drawing.Point(477, 354);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 27);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 354);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(471, 27);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // rtxMessages
            // 
            this.rtxMessages.BackColor = System.Drawing.Color.White;
            this.rtxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtxMessages.Location = new System.Drawing.Point(0, 0);
            this.rtxMessages.Name = "rtxMessages";
            this.rtxMessages.ReadOnly = true;
            this.rtxMessages.Size = new System.Drawing.Size(471, 349);
            this.rtxMessages.TabIndex = 0;
            this.rtxMessages.Text = "";
            this.rtxMessages.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxMessages_LinkClicked);
            this.rtxMessages.TextChanged += new System.EventHandler(this.rtxMessages_TextChanged);
            // 
            // ilTabPageLogo
            // 
            this.ilTabPageLogo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabPageLogo.ImageStream")));
            this.ilTabPageLogo.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabPageLogo.Images.SetKeyName(0, "globalChat");
            this.ilTabPageLogo.Images.SetKeyName(1, "privateChat");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 411);
            this.Controls.Add(this.tabMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.tabMain.ResumeLayout(false);
            this.tpGlobalChat.ResumeLayout(false);
            this.tpGlobalChat.PerformLayout();
            this.cmsOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tpGlobalChat;
        public System.Windows.Forms.RichTextBox rtxMessages;
        public System.Windows.Forms.TextBox txtMessage;
        public System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ImageList ilTabPageLogo;
        public System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip cmsOptions;
        private System.Windows.Forms.ToolStripMenuItem privateChatTsmi;
        public System.Windows.Forms.TabControl tabMain;
        public System.Windows.Forms.ImageList illvUsers;
        private System.Windows.Forms.ToolStripMenuItem blockTsmi;
        private System.Windows.Forms.ToolStripMenuItem unblockTsmi;
    }
}