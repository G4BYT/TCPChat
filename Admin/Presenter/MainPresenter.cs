using System;
using System.Drawing;
using System.Windows.Forms;
using Admin.View;
using Admin.Model;

namespace Admin.Presenter
{
    class MainPresenter
    {
        private readonly IMainView _view;
        private readonly MainModel _model;
        
        public MainPresenter(IMainView view, MainModel model)
        {
            _view = view;
            _view.AddIPClick += AddIPClick;
            _view.KeyDownPress += KeyDownPress;
            _view.MainTextChanged += MainTextChanged;
            _view.OpenLink += OpenLink;
            _view.OptionsClick += OptionsClick;
            _view.Send += Send;

            _model = model;
        }

        #region Events Handlers
        ///////////////////////////////////////////////////////////////////////
        // This region is for events handlers that are invoked from the view //
        ///////////////////////////////////////////////////////////////////////

        public void AddIPClick(object sender, EventArgs e)
        {
            Data data = (Data)sender;
            switch (data.option)
            {
                case "unbanTsmi":
                    CMD.SendCommand(Admin.socket, "UnBan|" + data.name);
                    break;

                case "addIPTsmi":
                    BanIPModel model = new BanIPModel();
                    BanIP view = new BanIP();
                    BanIPPresenter presenter = new BanIPPresenter(view, model, this);
                    view.ShowDialog();
                    if (presenter.IP == null)
                        return;
                    CMD.SendCommand(Admin.socket, "Ban|" + presenter.IP.ToString());
                    break;
            }

        }

        public void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send(sender, EventArgs.Empty);
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
            string command;
            string[] users = data.name.Split('|');
            switch (data.option)
            {
                case "kickTsmi":
                    command = "Kick";
                    foreach (string user in users)
                        command += "|" + user;
                    CMD.SendCommand(Admin.socket, command);
                    break;

                case "muteTsmi1":
                    command = "Mute";
                    foreach (string user in users)
                        command += "|" + user;
                    CMD.SendCommand(Admin.socket, command);
                    break;

                case "unmuteTsmi1":
                    command = "UnMute";
                    foreach (string user in users)
                        command += "|" + user;
                    CMD.SendCommand(Admin.socket, command);
                    break;

                case "banTsmi":
                    command = "Ban";
                    string strHolder;
                    foreach (string user in users)
                    {
                        strHolder = user;
                        strHolder = strHolder.Remove(strHolder.IndexOf(':'), strHolder.Length - strHolder.IndexOf(':'));
                        command += "|" + strHolder;
                    }
                    CMD.SendCommand(Admin.socket, command);
                    break;
            }
        }

        public void Send(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_view.MessageBar))
            {
                CMD.SendCommand(Admin.socket, "ServerGlobalMessage|" + _view.MessageBar);
                ServerMessage(_view.MessageBar);
            }
            _view.MessageBar = null;
        }
        #endregion

        #region Wrappers for helper methods from the view
        ////////////////////////////////////////////////////////////////////////////////////
        // This region is for wrappers of helper methods from the view                    //
        // That ensures if something is drastically changed in one of methods in the view //
        // The rest of program don't need a major rework, just the methods in this region //
        ////////////////////////////////////////////////////////////////////////////////////

        public void ServerMessage(string message)
        {
            _view.SpecialMessageDelegate?.Invoke("Server", Color.DarkRed, message);
        }

        public void NormalMessage(string name, string message)
        {
            _view.NormalMessageDelegate(name, message);
        }

        public void AddUser(string name)
        {
            _view.AddUser(name);
        }

        public bool ContainsUser(string name)
        {
            return _view.ContainsUser(name);
        }

        public void RemoveUser(string name)
        {
            _view.RemoveUser(name);
        }

        public void ChangeUserIcon(string name, IconColor iconColor)
        {
            _view.ChangeUserIcon(name, iconColor);
        }

        public void SetSendButtonEnabled(bool value)
        {
            _view.ButtonSendEnable = value;
        }

        public void SetLastMessage(string name, string message)
        {
            _view.SetLastMessage(name, message);
        }

        public void SetLastMessageTime(string name, string time)
        {
            _view.SetLastMessageTime(name, time);
        }

        public bool ContainsBanIP(string ip)
        {
            return _view.ContainsBanIP(ip);
        }

        public void AddBanIP(string ip, string dateTime)
        {
            _view.AddBanIP(ip, dateTime);
        }

        public void RemoveBanIP(string ip)
        {
            _view.RemoveBanIP(ip);
        }
        #endregion
    }
}
