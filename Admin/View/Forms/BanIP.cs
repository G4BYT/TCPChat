using System;
using System.Windows.Forms;

namespace Admin.View
{
    public partial class BanIP : Form, IBanIPView
    {
        public event EventHandler BanIPClick;

        public string IP
        {
            get { return txtIP.Text; }
            set { txtIP.Text = value; }
        }

        public BanIP()
        {
            InitializeComponent();
        }

        private void btnAddIP_Click(object sender, EventArgs e)
        {
            BanIPClick?.Invoke(sender, e);
        }

        public void CloseForm()
        {
            Close();
        }
    }
}
