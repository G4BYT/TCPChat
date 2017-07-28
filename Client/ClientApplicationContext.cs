using System.Windows.Forms;
using Client.View;
using Client.Model;

namespace Client
{
    class ClientApplicationContext : ApplicationContext
    {
        Connect _connectView;
        Main _mainView;

        public ClientApplicationContext(Connect connect, Main main)
        {
            _connectView = connect;
            _connectView.FormClosed += _connectView_FormClosed;

            _mainView = main;
            _mainView.FormClosed += _mainView_FormClosed;

            LoadForms();
        }

        private void _connectView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ConnectModel.Valid.Equals("Valid"))
                Application.Exit();
        }

        private void _mainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void LoadForms()
        {
            _connectView.ShowDialog();
            if (ConnectModel.Valid.Equals("Valid"))
                _mainView.Show();
        }
    }
}
