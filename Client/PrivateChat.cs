using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Client.View;

namespace Client
{
    class PrivateChat
    {
        static Main main;
        public delegate void SpecialMessageRichTextBox(RichTextBox richTextBox, string name, Color color, string text);
        public delegate void NormalMessageRichTextBox(RichTextBox richTextBox, string name, string text);
        public static SpecialMessageRichTextBox SpecialMessageDelegate = new SpecialMessageRichTextBox(SpecialMessage);
        public static NormalMessageRichTextBox NormalMessageDelegate = new NormalMessageRichTextBox(NormalMessage);

        public static Dictionary<string, bool> nicknameDictionary = new Dictionary<string, bool>();
        public static List<string> nicknameList = new List<string>();
        public static List<TabPage> tabPageList = new List<TabPage>();
        public static List<RichTextBox> richTextBoxList = new List<RichTextBox>();
        public static List<ListView> listViewList = new List<ListView>();
        public static List<TextBox> textBoxList = new List<TextBox>();
        public static List<Button> buttonList = new List<Button>();
        public static List<int> UnreadMessagesList = new List<int>();

        public static void InitMain(Main mainV)
        {
            main = mainV;
        }

        public static TabPage AddTabPage(string name)
        {
            TabPage tabPage = new TabPage(name);
            nicknameList.Add(name);
            tabPage.Name = name;
            tabPage.ImageIndex = 1;
            UnreadMessagesList.Add(0);
            AddControls(tabPage);
            tabPageList.Add(tabPage);

            return tabPage;
        }

        public static void AddControls(TabPage tabPage)
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.ReadOnly = true;
            richTextBox.BackColor = Color.White;
            richTextBox.Location = new Point(0, 0);
            richTextBox.Width = 471;
            richTextBox.Height = 349;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            richTextBox.DetectUrls = true;
            richTextBox.LinkClicked += RichTextBox_LinkClicked;
            richTextBoxList.Add(richTextBox);
            tabPage.Controls.Add(richTextBox);

            ListView listView = new ListView();
            listView.View = System.Windows.Forms.View.Details;
            listView.MultiSelect = false;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.Columns.Add("Nickname", 116);
            listView.Location = new Point(477, 0);
            listView.Width = 120;
            listView.Height = 348;
            listView.SmallImageList = main.illvUsers;
            listView.ColumnWidthChanging += ListView_ColumnWidthChanging;
            listView.Items.Add((ListViewItem)main.lvUsers.Items[0].Clone());
            listView.Items[0].ImageKey = "green";
            listView.Items.Add((ListViewItem)main.lvUsers.Items[main.lvUsers.Items.IndexOfKey(tabPage.Name)].Clone());
            listView.Items[1].ImageKey = "green";
            listViewList.Add(listView);
            tabPage.Controls.Add(listView);

            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Location = new Point(0, 354);
            textBox.Width = 471;
            textBox.Height = 27;
            textBox.KeyDown += TextBox_KeyDown;
            textBoxList.Add(textBox);
            tabPage.Controls.Add(textBox);

            Button button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);
            button.Location = new Point(477, 354);
            button.Width = 120;
            button.Height = 27;
            button.Text = "Send";
            button.Click += Button_Click;        
            buttonList.Add(button);
            tabPage.Controls.Add(button);
        }

        private static void RichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private static void Button_Click(object sender, EventArgs e)
        {

            int index;
            RichTextBox richTextBox;
            Button button = sender as Button;
            index = buttonList.IndexOf(button);
            if(string.IsNullOrWhiteSpace(textBoxList[index].Text))
            {
                textBoxList[index].Text = null;
                return;
            }

            richTextBox = richTextBoxList[index];
            richTextBox.Invoke(NormalMessageDelegate, new object[]
            {richTextBox, main.lvUsers.Items[0].Text,  textBoxList[index].Text});
            CMD.SendCommand("PrivateMessage|" + nicknameList[index] +
                "|" + textBoxList[index].Text);
            textBoxList[index].Text = null;
        }

        private static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int index;
            TextBox textBox = sender as TextBox;
            index = textBoxList.IndexOf(textBox);
            Button button = buttonList[index];
            if (e.KeyCode == Keys.Enter)
            {
                button.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private static void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            ListView listView = sender as ListView; 
            e.Cancel = true;
            e.NewWidth = listView.Columns[e.ColumnIndex].Width;
        }

        private static void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }

        public static void SpecialMessage(RichTextBox richTextBox, string name, Color color, string text)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(name);
            richTextBox.SelectionColor = richTextBox.ForeColor;
            richTextBox.AppendText(": " + text);
            richTextBox.AppendText(Environment.NewLine);
        }

        public static void NormalMessage(RichTextBox richTextBox, string name, string text)
        {
            richTextBox.AppendText(name + ": " + text);
            richTextBox.AppendText(Environment.NewLine);
        }

        public static void RemoveTabPage(string name)
        {
            int index = nicknameList.IndexOf(name);

            nicknameList.RemoveAt(index);
            buttonList.RemoveAt(index);
            textBoxList.RemoveAt(index);
            listViewList.RemoveAt(index);
            richTextBoxList.RemoveAt(index);
            tabPageList.RemoveAt(index);
            UnreadMessagesList.RemoveAt(index);
        }

        public static void AddUnreadMessageCount(string name)
        {
            TabPage tabPage1;
            if (main.tabMain.SelectedTab != main.tabMain.TabPages[main.tabMain.TabPages.IndexOfKey(name)])
            {
                tabPage1 = main.tabMain.TabPages[main.tabMain.TabPages.IndexOfKey(name)];
                if (tabPage1.Text.Contains("["))
                    tabPage1.Text =
                        tabPage1.Text.Remove(tabPage1.Text.IndexOf('[') - 1);

                int indexTabPage;
                indexTabPage = nicknameList.IndexOf(name);
                UnreadMessagesList[indexTabPage]++;
                tabPage1.Text +=
                    " [" + UnreadMessagesList[indexTabPage] + "]";
            }
        }
    }
}
