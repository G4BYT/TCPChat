using System;
using System.Net;
using System.Windows.Forms;
using Admin.View;
using Admin.Model;

namespace Admin.Presenter
{
    class BanIPPresenter
    {
        private readonly BanIP _view;
        private readonly BanIPModel _model;
        private readonly MainPresenter _mainPresenter;

        public IPAddress IP { get; private set; }

        public BanIPPresenter(BanIP view, BanIPModel model, MainPresenter main)
        {
            _view = view;
            _view.BanIPClick += BanIPClick;

            _model = model;

            _mainPresenter = main;
        }

        private void BanIPClick(object sender, EventArgs e)
        {
            try
            {
                IP = IPAddress.Parse(_view.IP);
            }
            catch
            {
                MessageBox.Show("Invalid IP address.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                IP = null;
                return;
            }

            if (_mainPresenter.ContainsBanIP(IP.ToString()))
            {
                MessageBox.Show("IP address already banned.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                IP = null;
                return;
            }
            _view.CloseForm();
        }
    }
}
