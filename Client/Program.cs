using System;
using System.Windows.Forms;
using Client.View;
using Client.Model;
using Client.Presenter;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ///////////////////////////////////////////////////////
            // Create model, view and presenter for connect form //
            ///////////////////////////////////////////////////////
            var connectModel = new ConnectModel();
            var connectView = new Connect();
            var connectPresenter = new ConnectPresenter(connectView, connectModel);

            //////////////////////////////////////////////////////////////////////////////////////////
            // Create model, view and presenter for main form. Also, initialize CMD and PrivateChat //
            //////////////////////////////////////////////////////////////////////////////////////////
            var mainModel = new MainModel();
            var mainView = new Main();
            var mainPresenter = new MainPresenter(mainView, mainModel);
            CMD.InitPresenter(mainPresenter);
            CMDUser.InitPresenter(mainPresenter);
            PrivateChat.InitMain(mainView);

            Application.Run(new ClientApplicationContext(connectView, mainView));
        }
    }
}
