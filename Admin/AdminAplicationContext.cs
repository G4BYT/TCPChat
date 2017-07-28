using System.Windows.Forms;
using Admin.View;
using Admin.Model;

namespace Admin
{
    class AdminAplicationContext : ApplicationContext
    {
        private readonly Main _mainView;
        private readonly IPPort _ipportView;
        private readonly Credentials _credentialsView;

        public AdminAplicationContext(Main main, IPPort ipport, Credentials credentials)
        {
            _mainView = main;
            _mainView.FormClosed += _mainView_FormClosed;

            _ipportView = ipport;
            _ipportView.FormClosed += _ipportView_FormClosed;

            _credentialsView = credentials;
            _credentialsView.FormClosed += _credentialsView_FormClosed;

            LoadForms();
        }

        private void _mainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void _ipportView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!IPPortModel.Result)
                Application.Exit();
        }

        private void _credentialsView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!CredentialsModel.ValidCredentials)
                Application.Exit();
        }

        public void LoadForms()
        {
            _ipportView.ShowDialog();
            if (!IPPortModel.Result)
            {
                Application.Exit();
                return;
            }
            _credentialsView.ShowDialog();
            if (!CredentialsModel.ValidCredentials)
            {
                Application.Exit();
                return;
            }
            _mainView.Show();
        }
    }
}
