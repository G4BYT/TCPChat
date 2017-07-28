using System;
using System.Windows.Forms;
using System.Drawing;

namespace Admin.View
{
    public partial class Main : Form, IMainView
    {
        public event EventHandler Send;
        public event EventHandler<KeyEventArgs> KeyDownPress;
        public event EventHandler OpenLink;
        public event EventHandler MainTextChanged;
        public event EventHandler OptionsClick;
        public event EventHandler AddIPClick;

        public SpecialMessage SpecialMessageDelegate { get; set; }
        public NormalMessage NormalMessageDelegate { get; set; }

        public string MessageBar
        {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value; }
        }

        public bool ButtonSendEnable
        {
            get { return btnSendMessage.Enabled; }
            set { btnSendMessage.Enabled = value; }
        }

        public Main()
        {
            InitializeComponent();
            SpecialMessageDelegate = new SpecialMessage(SpecialMessage);
            NormalMessageDelegate = new NormalMessage(NormalMessage);
        }

        #region Events 
        //////////////////////////////////////////////////
        // This region is for events handlers from form //
        //////////////////////////////////////////////////

        private void btnSendMessage_Click(object sender, EventArgs e)
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

        private void rtxMessages_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            OpenLink?.Invoke(e.LinkText, EventArgs.Empty);
        }

        private void commandOptions(object sender, EventArgs e)
        {
            if (lvUsersPanel.SelectedItems.Count == 0)
               return;
            ListView.SelectedListViewItemCollection selectedItems = lvUsersPanel.SelectedItems;
            string users = string.Empty;
            foreach(ListViewItem item in selectedItems)
            {
                users += "|" + item.SubItems[0].Text;
            }
            string name = ((ToolStripItem)sender).Name;
            OptionsClick?.Invoke(new Data(users.Substring(1), name), e);
        }

        private void addIPTsmi_Click(object sender, EventArgs e)
        {
            ToolStripItem tsItem = sender as ToolStripItem;
            ListView.SelectedListViewItemCollection selectedItems = lvBanList.SelectedItems;
            string lvItems = null;
            foreach (ListViewItem lvItem in selectedItems)
            {
                lvItems += "|" + lvItem.Text;
            }
            Data data = new Data(lvItems?.Substring(1), tsItem.Name);
            AddIPClick?.Invoke(data, e);
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

        public void AddUser(string name)
        {
            string[] names = name.Split('|');
            for (int i = 0; i < names.Length; i+= 5)
            {
                if (lvUsers.Items.ContainsKey(names[i]))
                    continue;
                ListViewItem item = new ListViewItem(names[i], names[i + 1])
                {
                    Name = names[i]
                };
                lvUsers.Items.Add(item);
                item = new ListViewItem();
                item.Text = names[i + 2];
                item.Name = names[i];
                item.SubItems.Add(names[i]);
                item.SubItems.Add(names[i + 3]);
                item.SubItems.Add(names[i + 4]);
                lvUsersPanel.Items.Add(item);
            }
        }

        public bool ContainsUser(string name)
        {
            return lvUsers.Items.ContainsKey(name);
        }

        public void RemoveUser(string name)
        {
            lvUsers.Items.RemoveByKey(name);
            lvUsersPanel.Items.RemoveByKey(name);
        }

        public void GoToLastMessage()
        {
            rtxMessages.SelectionStart = rtxMessages.Text.Length;
            rtxMessages.ScrollToCaret();
        }

        public void SetLastMessage(string name, string lastmessage)
        {
            if (!lvUsers.Items.ContainsKey(name))
                return;
            ListViewItem item = lvUsersPanel.Items[lvUsersPanel.Items.IndexOfKey(name)];
            item.SubItems[3].Text = lastmessage;
        }

        public void SetLastMessageTime(string name, string time)
        {
            if (!lvUsers.Items.ContainsKey(name))
                return;
            ListViewItem item = lvUsersPanel.Items[lvUsersPanel.Items.IndexOfKey(name)];
            item.SubItems[2].Text = time;
        }

        public bool ContainsBanIP(string ip)
        {
            for (int i = 0; i < lvBanList.Items.Count; i++)
            {
                if (lvBanList.Items[i].SubItems[0].Equals(ip))
                    return true;
            }
            return false;
        }

        public void AddBanIP(string ip, string dateTime)
        {
            ListViewItem item = new ListViewItem();
            if (lvBanList.Items.ContainsKey(ip))
            {
                item = lvBanList.Items[lvBanList.Items.IndexOfKey(ip)];
                item.SubItems[1].Text = dateTime;
                return;
            }

            item = new ListViewItem(ip);
            item.SubItems.Add(dateTime);
            item.Name = ip;
            lvBanList.Items.Add(item);
        }

        public void RemoveBanIP(string ip)
        {
            lvBanList.Items.RemoveByKey(ip);
        }
        #endregion
    }
}