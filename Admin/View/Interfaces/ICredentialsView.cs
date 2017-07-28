using System;

namespace Admin.View
{
    interface ICredentialsView
    {
        event EventHandler ConnectClick;

        string Username { get; set; }
        string Password { get; set; }

        void CloseForm();
    }
}
