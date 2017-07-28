using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client.View
{
    public partial class Main : Form, IMainView
    {
        public event EventHandler<KeyEventArgs> KeyDownPress;
        public event EventHandler MainTextChanged;
        public event EventHandler OpenLink;
        public event EventHandler OptionsClick;
        public event EventHandler Send;
        public event EventHandler TabChanged;

        public int GlobalChatUnreadMessagesCount { get; set; }
        public SpecialMessage SpecialMessageDelegate { get; set; }
        public NormalMessage NormalMessageDelegate { get; set; }

        public string MessageBar
        {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value; }
        }

        public string SelectedTab
        {
            get { return tabMain.SelectedTab.Name; }
            set { tabMain.SelectedTab = tabMain.TabPages[tabMain.TabPages.IndexOfKey(value)]; }
        }

        public bool ButtonSendEnable
        {
            get { return btnSend.Enabled; }
            set { btnSend.Enabled = value; }

        }

        public Main()
        {
            SpecialMessageDelegate = new SpecialMessage(SpecialMessage);
            NormalMessageDelegate = new NormalMessage(NormalMessage);
            InitializeComponent();
        }

        #region Events Handlers
        //////////////////////////////////////////////////
        // This region is for events handlers from form //
        //////////////////////////////////////////////////

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send?.Invoke(sender, e);
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownPress?.Invoke(sender, e);
        }

        private void rtxMessages_TextChanged(object sender, EventArgs e)
        {
            MainTextChanged?.Invoke(sender, e);
        }

        private void lvUsers_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvUsers.Columns[e.ColumnIndex].Width;
        }

        private void cmsOptions_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count == 0 || lvUsers.SelectedItems[0].Index == 0)
                return;

            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Data data = new Data(lvUsers.SelectedItems[0].Text, item.Name);
            OptionsClick?.Invoke(data, e);
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabChanged?.Invoke(tabMain.SelectedTab.Name, e);
        }

        private void rtxMessages_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            OpenLink?.Invoke(e.LinkText, EventArgs.Empty);
        }
        #endregion

        #region Helper Methods
        ///////////////////////////////////////////////////////////////////////////////////
        // This region is for methods that will be used in presenter to update the view. //
        ///////////////////////////////////////////////////////////////////////////////////

        public void SpecialMessage(string name, Color color, string text)
        {
                rtxMessages.SelectionStart = rtxMessages.TextLength;
                rtxMessages.SelectionColor = color;
                rtxMessages.AppendText(name);
                rtxMessages.SelectionColor = rtxMessages.ForeColor;
                rtxMessages.AppendText(": " + text);
                rtxMessages.AppendText(Environment.NewLine);
        }

        public void NormalMessage(string name, string text)
        {
                rtxMessages.AppendText(name + ": " + text);
                rtxMessages.AppendText(Environment.NewLine);
        }

        public void GoToLastMessage()
        {
            rtxMessages.SelectionStart = rtxMessages.Text.Length;
            rtxMessages.ScrollToCaret();
        }

        public void RemoveUnseenMessageCount(string name)
        {
            TabPage tabPage;
            if (name.Equals("tpGlobalChat"))
            {
                if (GlobalChatUnreadMessagesCount == 0)
                    return;
                tabPage = tabMain.TabPages[0];
                tabPage.Text = tabPage.Text.Remove(tabPage.Text.IndexOf('[') - 1);
                GlobalChatUnreadMessagesCount = 0;
            }
            else
            {
                tabPage = tabMain.TabPages[tabMain.TabPages.IndexOfKey(name)];
                int index = PrivateChat.nicknameList.IndexOf(tabPage.Name);
                if (PrivateChat.UnreadMessagesList[index] == 0)
                    return;
                tabPage.Text = tabPage.Text.Remove(tabPage.Text.IndexOf('[') - 1);
                PrivateChat.UnreadMessagesList[index] = 0;
            }
        }

        public void RemoveTab(string name)
        {
            if (tabMain.TabPages.ContainsKey(name))
            {
                tabMain.TabPages.RemoveByKey(name);
            }
        }

        public bool ContainsTab(string name)
        {
            return tabMain.TabPages.ContainsKey(name);
        }

        public void AddGlobalChatTabNotification(int number)
        {
            TabPage tabPage = tabMain.TabPages[0];
            if (tabPage.Text.Contains("["))
                tabPage.Text =
                         tabPage.Text.Remove(tabPage.Text.IndexOf('[') - 1);
            tabMain.TabPages[0].Text += " [" + number + "]";
        }

        public void AddUser(string name, string icon)
        {
            ListViewItem item = new ListViewItem(name, icon);
            item.Name = name;
            lvUsers.Items.Add(item);
        }

        public bool ContainsUser(string name)
        {
            return lvUsers.Items.ContainsKey(name);
        }

        public void RemoveUser(string name)
        {
            lvUsers.Items.RemoveByKey(name);
        }

        public void ChangeUserIcon(string name, IconColor iconColor)
        {
            ListViewItem item = lvUsers.Items[lvUsers.Items.IndexOfKey(name)];
            switch (iconColor)
            {
                case IconColor.Green:
                    item.ImageKey = "green";
                    break;

                case IconColor.Yellow:
                    item.ImageKey = "yellow";
                    break;

                case IconColor.Red:
                    item.ImageKey = "red";
                    break;
            }
        }

        public void AddTab(string name)
        {
            tabMain.TabPages.Add(PrivateChat.AddTabPage(name));
        }

        public void ClearGlobalChatRoom()
        {
            rtxMessages.Text = null;
        }
        #endregion
    }
}
