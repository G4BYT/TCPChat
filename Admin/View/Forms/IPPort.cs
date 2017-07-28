using System;
using System.Windows.Forms;

namespace Admin.View
{
    public partial class IPPort : Form, IIPPortView
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

        public IPPort()
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
