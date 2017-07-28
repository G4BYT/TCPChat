using System.Net.Sockets;
using Admin.Presenter;
using Admin.Model;

namespace Admin
{
   static class CMD
    {
        private static MainPresenter _presenter;

        public static void InitPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        public static void ReceiveCommand(string command)
        {
            string[] commands = command.Split('|');
            switch (commands[0])
            {
                case "UsernamePasswordAccepted":
                    CredentialsModel.ValidCredentials = true;
                    CredentialsModel.ValidString = "Valid";
                    if (commands.Length == 1)
                        break;
                    _presenter.AddUser(command.Substring(commands[0].Length + 1));
                    break;

                case "UsernamePasswordRejected":
                    CredentialsModel.ValidCredentials = false;
                    CredentialsModel.ValidString = "Invalid";
                    break;

                case "GlobalMessage":
                    _presenter.NormalMessage(commands[1], commands[2]);
                    _presenter.SetLastMessageTime(commands[1], commands[3]);
                    _presenter.SetLastMessage(commands[1], commands[2]);
                    break;

                case "NewUser":
                    if(!_presenter.ContainsUser(commands[1]))
                    {
                        _presenter.AddUser(command.Substring(commands[0].Length + 1));
                    }
                    _presenter.ServerMessage(commands[1] + " has been connected");
                    break;

                case "Disconnected":
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been disconnect");
                    break;

                case "UserIdle":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Yellow);
                    break;

                case "UserActive":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Green);
                    break;

                case "UserKicked":
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been kicked");
                    break;

                case "UserMuted":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Red);
                    _presenter.ServerMessage(commands[1] + " has been muted");
                    break;

                case "UserUnmuted":
                    _presenter.ChangeUserIcon(commands[1], View.IconColor.Green);
                    _presenter.ServerMessage(commands[1] + " has been unmuted");
                    break;

                case "Banned":
                    _presenter.RemoveUser(commands[1]);
                    _presenter.ServerMessage(commands[1] + " has been banned");
                    break;

                case "BanIP":
                    if (commands.Length >= 3)
                        for(int i = 1; i < commands.Length; i+= 2)
                        _presenter.AddBanIP(commands[i], commands[i+1]);
                    else
                        _presenter.AddBanIP(commands[1], " ");
                    break;

                case "UnBan":
                    for (int i = 1; i < commands.Length; i++)
                    {
                        _presenter.RemoveBanIP(commands[i]);
                    }
                    break;
            }
        }

        public static void SendCommand(Socket sender, string command)
        {
            byte[] buffer = Cryptography.Encrypt(command);
            try
            {
                sender.Send(buffer, 0, buffer.Length, 0);
            }
            catch
            {

            }
        }
    }
}
