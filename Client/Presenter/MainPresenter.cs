using System;
using System.Windows.Forms;
using System.Drawing;
using Client.View;
using Client.Model;

namespace Client.Presenter
{
    class MainPresenter
    {
        private readonly IMainView _view;
        private readonly MainModel _model;

        public MainPresenter(IMainView view, MainModel model)
        {
            _view = view;
            _view.KeyDownPress += KeyDownPress;
            _view.MainTextChanged += MainTextChanged;
            _view.OpenLink += OpenLink;
            _view.OptionsClick += OptionsClick;
            _view.Send += Send;
            _view.TabChanged += TabChanged;

            _model = model;
        }

        #region Events Handlers
        ///////////////////////////////////////////////////////////////////////
        // This region is for events handlers that are invoked from the view //
        ///////////////////////////////////////////////////////////////////////

        public void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Client.muted == true)
            {
                e.SuppressKeyPress = true;
                _view.MessageBar = null;
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                Send(sender, EventArgs.Empty);
                _view.MessageBar = null;
                e.SuppressKeyPress = true;
            }
        }

        public void MainTextChanged(object sender, EventArgs e)
        {
            _view.GoToLastMessage();
        }

        public void OpenLink(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start((string)sender);
        }

        public void OptionsClick(object sender, EventArgs e)
        {
            Data data = (Data)sender;
            bool isBlocked = false;

            switch (data.option)
            {
                case "privateChatTsmi":
                    PrivateChat.nicknameDictionary.TryGetValue(data.name, out isBlocked);
                    if (isBlocked == true)
                    {
                        _view.SpecialMessageDelegate?.Invoke("Server", Color.DarkRed, " You need to unblock " + data.name +
                            " before opening private chat");
                        return;
                    }

                    if (_view.ContainsTab(data.name))
                    {
                        _view.SelectedTab = data.name;
                        return;
                    }

                    CMD.SendCommand("PrivateChat|" + data.name);
                    break;

                case "blockTsmi":
                    PrivateChat.nicknameDictionary.TryGetValue(data.name, out isBlocked);
                    if (isBlocked == true)
                        return;
                    PrivateChat.nicknameDictionary[data.name] = true;
                    CMD.SendCommand("Blocked|" + data.name);
                    if (_view.ContainsTab(data.name))
                    {
                        _view.RemoveTab(data.name);
                        PrivateChat.RemoveTabPage(data.name);
                    }
                    break;

                case "unblockTsmi":
                    PrivateChat.nicknameDictionary.TryGetValue(data.name, out isBlocked);
                    if (isBlocked == false)
                        return;
                    PrivateChat.nicknameDictionary[data.name] = false;
                    break;
            }
        }

        public void Send(object Sender, EventArgs e)
        {
            if (Client.muted == true)
                _view.MessageBar = null;

            if (!string.IsNullOrWhiteSpace(_view.MessageBar) && !CMDUser.IsCommand(_view.MessageBar))
                CMD.SendCommand("GlobalMessage|" + Client.nickname + "|" + _view.MessageBar);
            _view.MessageBar = null;
        }

        public void TabChanged(object sender, EventArgs e)
        {
            string name = (string)sender;
            _view.RemoveUnseenMessageCount((string)sender);
        }
        #endregion

        #region Wrappers for helper methods from the view
        ////////////////////////////////////////////////////////////////////////////////////
        // This region is for wrappers of helper methods from the view                    //
        // That ensures if something is drastically changed in one of methods in the view //
        // The rest of program don't need a major rework, just the methods in this region //
        ////////////////////////////////////////////////////////////////////////////////////

        public void BotCommand(string command)
        {
            if(command.Equals("/clear"))
            {
                _view.ClearGlobalChatRoom();
                return;
            }

            _view.SpecialMessageDelegate?.Invoke("Bot: ", Color.DarkBlue, command);
        }

        public void ServerMessage(string message)
        {
            GlobalChatNotification();
            _view.SpecialMessageDelegate?.Invoke("Server", Color.DarkRed, message);
        }

        public void NormalMessage(string name, string message)
        {
            GlobalChatNotification();
            _view.NormalMessageDelegate(name, message);
        }

        public void GlobalChatNotification()
        {
            if (_view.SelectedTab.Equals("tpGlobalChat"))
                return;
            _view.GlobalChatUnreadMessagesCount = _view.GlobalChatUnreadMessagesCount + 1;
            _view.AddGlobalChatTabNotification(_view.GlobalChatUnreadMessagesCount);
        }

        public void AddUser(string name, string icon)
        {
            _view.AddUser(name, icon);
        }

        public bool ContainsUser(string name)
        {
            return _view.ContainsUser(name);
        }

        public void RemoveUser(string name)
        {
            _view.RemoveUser(name);
        }

        public bool ContainsTab(string name)
        {
            return _view.ContainsTab(name);
        }

        public void RemoveTab(string name)
        {
            _view.RemoveTab(name);
        }

        public void ChangeUserIcon(string name, IconColor iconColor)
        {
            _view.ChangeUserIcon(name, iconColor);
        }

        public void SetSendButtonEnabled(bool value)
        {
            _view.ButtonSendEnable = value;
        }

        public void AddTab(string name)
        {
            _view.AddTab(name);
        }
        #endregion
    }
}
