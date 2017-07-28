using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Admin.Model;
using Admin.View;

namespace Admin.Presenter
{
    class CredentialsPresenter
    {
        private readonly Credentials _view;
        private readonly CredentialsModel _model;

        public CredentialsPresenter(Credentials view, CredentialsModel model)
        {
            _view = view;
            _view.ConnectClick += ConnectClick;
#if DEBUG
            _view.Username = "gabi";
            _view.Password = "gabi";
#endif

            _model = model;
        }

        private async void ConnectClick(object sender, EventArgs e)
        {
            CredentialsModel.ValidString = " ";
            CMD.SendCommand(Admin.socket, "CheckUsernamePassword|" + _view.Username + "|" + _view.Password);
            await Task.Factory.StartNew(
                () =>
                {
                    while (CredentialsModel.ValidString == " ")
                    {
                        Thread.Sleep(1);
                    }
                });
            if (CredentialsModel.ValidString == "Valid")
                _view.CloseForm();
            else
                MessageBox.Show("Invalid username and password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
