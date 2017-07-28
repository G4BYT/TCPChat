using System;

namespace Admin.View
{
    interface IIPPortView
    {
        event EventHandler ConnectClick;

        string IP { get; set; }
        string Port { get; set; }

        void CloseForm();
    }
}
