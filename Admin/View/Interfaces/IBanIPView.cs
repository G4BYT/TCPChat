using System;

namespace Admin.View
{
    interface IBanIPView
    {
        event EventHandler BanIPClick;

        string IP { get; set; }

        void CloseForm();
    } 
}
