using System;
using System.Windows.Forms;

namespace Admin.View
{
    public partial class Credentials : Form, ICredentialsView
    {
        public event EventHandler ConnectClick;

        public string Username
        {
            get { return txtUsername.Text; }
            set { txtUsername.Text = value; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public Credentials()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectClick?.Invoke(sender, e);
        }

        public void CloseForm()
        {
            Close();
        }
    }
}
