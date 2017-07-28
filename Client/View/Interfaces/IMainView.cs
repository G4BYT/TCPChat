using System;
using System.Windows.Forms;
using System.Drawing;

namespace Client.View
{
    public delegate void SpecialMessage(string name, Color color, string text);
    public delegate void NormalMessage(string name, string text);

    public struct Data
    {
        public string name;
        public string option;
        public Data(string name, string option)
        {
            this.name = name;
            this.option = option;
        }
    }

    public enum IconColor
    {
        Green,
        Yellow,
        Red
    }

    interface IMainView
    {
        event EventHandler Send;
        event EventHandler<KeyEventArgs> KeyDownPress;
        event EventHandler OpenLink;
        event EventHandler MainTextChanged;
        event EventHandler TabChanged;
        event EventHandler OptionsClick;

        SpecialMessage SpecialMessageDelegate { get; set; }
        NormalMessage NormalMessageDelegate { get; set; }
        string MessageBar { get; set; }
        string SelectedTab { get; set; }
        bool ButtonSendEnable { get; set; }
        int GlobalChatUnreadMessagesCount { get; set; }

        void RemoveUnseenMessageCount(string name);
        void RemoveTab(string name);
        void ClearGlobalChatRoom();
        void ChangeUserIcon(string name, IconColor iconColor);
        bool ContainsTab(string name);
        void AddGlobalChatTabNotification(int number);
        void AddUser(string name, string icon);
        bool ContainsUser(string name);
        void RemoveUser(string name);
        void AddTab(string name);
        void GoToLastMessage();
    }
}
