using System;

namespace Client.View
{
    interface IConnectView
    {
        event EventHandler ConnectClick;

        string IP { get; set; }
        string Port { get; set; }
        string Nickname { get; set; }

        void CloseForm();
    }
}
