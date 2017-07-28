using System.Text;
using System.Windows.Forms;
using Client.Model;
using Client.Presenter;

namespace Client
{
    static class CMD
    {
        static MainPresenter _presenter;

        public static void InitPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        public static void SendCommand(string command)
        {
            byte[] data = Cryptography.Encrypt(command);
            Client.socket.Send(data);
        }

        public static void ReceiveCommand(string command)
        {
            int index;
            string[] commands = command.Split('|');
            switch (commands[0])
            {
                case "NicknameAvailable":
                    ConnectModel.Valid = "Valid";
                    break;

                case "NicknameAlreadyUse":
                    ConnectModel.Valid = "Invalid";
                    break;

                case "Connected":
                    for (int i = 1; i < commands.Length; i+=2)
                    {
                        if (!_presenter.ContainsUser(commands[i]))
                        {
                            if (!PrivateChat.nicknameDictionary.ContainsKey(commands[i]))
                                PrivateChat.nicknameDictionary.Add(commands[i], false);
                            _presenter.AddUser(commands[i], commands[i+1]);
                        }
                    }
                    _presenter.ServerMessage("You are now connected to server");
                    break;

                case "NewUser":
                    _presenter.AddUser(commands[1], "green");
                    _presenter.ServerMessage(commands[1] + " has been connected");
                    break;

                case "Disconnected":
                    PrivateChat.nicknameDictionary.Remove(commands[1]);

                    if (_presenter.ContainsTab(commands[1]))
                    {
                        _presenter.RemoveTab(commands[1]);
                        PrivateChat.RemoveTabPage(commands[1]);
                    }
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been disconnect");
                    break;

                case "GlobalMessage":
                    _presenter.NormalMessage(commands[1], commands[2]);
                    break;

                case "ServerGlobalMessage":
                    _presenter.ServerMessage(commands[1]);
                    break;

                case "Disconnect":
                    MessageBox.Show("You have been kicked from the server!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    break;

                case "UserKicked":
                    PrivateChat.nicknameDictionary.Remove(commands[1]);

                    if (_presenter.ContainsTab(commands[1]))
                    {
                        _presenter.RemoveTab(commands[1]);
                        PrivateChat.RemoveTabPage(commands[1]);
                    }
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been kicked");
                    break;

                case "Mute":
                    _presenter.ServerMessage("You have been muted");
                    _presenter.ChangeUserIcon(Client.nickname, View.IconColor.Red);
                    Client.muted = true;
                    _presenter.SetSendButtonEnabled(false);
                    break;

                case "Unmute":
                    _presenter.ServerMessage("You have been unmuted");
                    _presenter.ChangeUserIcon(Client.nickname, View.IconColor.Green);
                    Client.muted = false;
                    _presenter.SetSendButtonEnabled(true);
                    break;

                case "UserMuted":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Red);
                    _presenter.ServerMessage(commands[1] + " has been muted");
                    break;

                case "UserUnmuted":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Green);
                    _presenter.ServerMessage(commands[1] + " has been unmuted");
                    break;

                case "SameIPClient":
                    MessageBox.Show("You are already conected on the server!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;

                case "Ban":
                    MessageBox.Show("You have been banned from the server!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                    break;

                case "YouAreBanned":
                    MessageBox.Show("You are banned from the server!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;

                case "Banned":
                    PrivateChat.nicknameDictionary.Remove(commands[1]);

                    if (_presenter.ContainsTab(commands[1]))
                    {
                        _presenter.RemoveTab(commands[1]);
                        PrivateChat.RemoveTabPage(commands[1]);
                    }
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been banned");
                    break;

                case "UserIdle":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Yellow);
                    break;

                case "UserActive":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Green);
                    break;

                case "VerifyPrivateChat":
                    bool isBlocked;
                    PrivateChat.nicknameDictionary.TryGetValue(commands[1], out isBlocked);
                    if(isBlocked == false)
                    {
                        SendCommand("PrivateChatAccepted|" + commands[1]);
                    }
                    else
                    {
                        SendCommand("Blocked|" + commands[1]);
                        if (_presenter.ContainsTab(commands[1]))
                        {
                           _presenter.RemoveTab(commands[1]);
                            PrivateChat.RemoveTabPage(commands[1]);
                        }
                    }
                    break;

                case "PrivateChatAccepted":
                    _presenter.AddTab(commands[1]);
                    break;

                case "PrivateMessage":
                    index = -1;
                    foreach(var tabPage in PrivateChat.tabPageList)
                    {
                        if (tabPage.Name == commands[1])
                            index = PrivateChat.tabPageList.IndexOf(tabPage);
                    }
                    if (index == -1)
                    {
                        _presenter.AddTab(commands[1]);
                        index = PrivateChat.nicknameList.IndexOf(commands[1]);
                    }
                    PrivateChat.AddUnreadMessageCount(commands[1]);
                    PrivateChat.richTextBoxList[index].Invoke(PrivateChat.NormalMessageDelegate,
                        new object[] { PrivateChat.richTextBoxList[index], commands[1], commands[2] });
                    break;

                case "Blocked":
                    if (_presenter.ContainsTab(commands[1]))
                    {
                        _presenter.RemoveTab(commands[1]);
                        PrivateChat.RemoveTabPage(commands[1]);
                    }
                    _presenter.ServerMessage(commands[1] + " has been blocked you.");
                    break;
            }
        }
    }
}