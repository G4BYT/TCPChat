using System;
using System.Drawing;
using System.Windows.Forms;

namespace Admin.View
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
        event EventHandler OptionsClick;
        event EventHandler AddIPClick;

        SpecialMessage SpecialMessageDelegate { get; set; }
        NormalMessage NormalMessageDelegate { get; set; }
        string MessageBar { get; set; }
        bool ButtonSendEnable { get; set; }

        void AddUser(string name);
        void ChangeUserIcon(string name, IconColor iconColor);
        bool ContainsUser(string name);
        void RemoveUser(string name);
        void AddBanIP(string ip, string dateTime);
        bool ContainsBanIP(string ip);
        void RemoveBanIP(string ip);
        void SetLastMessage(string name, string lastmessage);
        void SetLastMessageTime(string name, string time);
        void GoToLastMessage();
    }
}
