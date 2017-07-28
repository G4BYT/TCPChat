using System;
using System.Windows.Forms;

namespace Client.View
{
    public partial class Connect : Form, IConnectView
    {
        public event EventHandler ConnectClick;

        public string IP
        {
            get { return txtIP.Text; }
            set { txtIP.Text = value; }
        }
        public string Port
        {
            get { return txtPort.Text; }
            set { txtPort.Text = value; }
        }
        public string Nickname
        {
            get { return txtNickname.Text; }
            set { txtNickname.Text = value; }
        }
        public Connect()
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
