using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using Client.View;
using Client.Model;
using System.Windows.Forms;

namespace Client.Presenter
{
    class ConnectPresenter
    {
        IConnectView _view;
        ConnectModel _model;
        string _pattern = @"^([A-Za-z0-9]+)$";

        public ConnectPresenter(IConnectView view, ConnectModel model)
        {
            _view = view;
            _view.ConnectClick += ConnectClick;
#if DEBUG
            _view.IP = "127.0.0.1";
            _view.Port = "3500";
            Random rnd = new Random();
            _view.Nickname = rnd.Next(100000000, 999999999).ToString();
#endif

            _model = model;
        }

        public async void ConnectClick(object sender, EventArgs e)
        {
            ConnectModel.Valid = " ";
            IPAddress ipadress;
            int port;
            Match nicknameMatch = Regex.Match(_view.Nickname, _pattern);

            try
            {
                ipadress = IPAddress.Parse(_view.IP);
            }
            catch
            {
                MessageBox.Show("Invalid IP Address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                port = int.Parse(_view.Port);
            }
            catch
            {
                MessageBox.Show("Invalid port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (port < 0 || port > 65535)
            {
                MessageBox.Show("Invalid port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!nicknameMatch.Success || _view.Nickname.Length < 3)
            {
                MessageBox.Show("Invalid nickname! Use only alphanumeric characters", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Client.nickname = _view.Nickname;
            if (_model.Result == false)
            {
                _model.Result = await Client.Connect(ipadress, port);
            }
            else
            {
                Client.SendNickname();
            }

            if (_model.Result == false)
                return;

            await Task.Factory.StartNew(() =>
            {
                while (ConnectModel.Valid == " ")
                {
                    Thread.Sleep(1);
                }
            });

            if (ConnectModel.Valid == "Valid")
            {
                CMD.SendCommand("Connected|" + _view.Nickname);
            }
            else
            {
                MessageBox.Show("Nickname already used.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_model.Result == true)
                _view.CloseForm();
        }
    }
}
