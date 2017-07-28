using System;
using System.Net;
using System.Windows.Forms;
using Admin.View;
using Admin.Model;

namespace Admin.Presenter
{
    class IPPortPresenter
    {
        private readonly IPPort _view;
        private readonly IPPortModel _model;

        public IPPortPresenter(IPPort view, IPPortModel model)
        {
            _view = view;
            _view.ConnectClick += _view_ConnectClick;
#if DEBUG
            _view.IP = "127.0.0.1";
            _view.Port = "3501";
#endif
            _model = model;
        }

        private async void _view_ConnectClick(object sender, EventArgs e)
        {
            int port = -1;
            IPAddress ipAddress;

            try
            {
                ipAddress = IPAddress.Parse(_view.IP);
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

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                MessageBox.Show("Invalid port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                IPPortModel.Result = await Admin.Connect(ipAddress, port);
                if (IPPortModel.Result)
                    _view.CloseForm();
            }
        }
    }
}