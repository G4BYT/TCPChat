using System;
using System.Windows.Forms;
using Admin.View;
using Admin.Model;
using Admin.Presenter;

namespace Admin
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
            var ipportView = new IPPort();
            var ipportModel = new IPPortModel();
            var ipportPresenter = new IPPortPresenter(ipportView, ipportModel);

            var credentialsView = new Credentials();
            var credentialsModel = new CredentialsModel();
            var credentialsPresenter = new CredentialsPresenter(credentialsView, credentialsModel);

            var mainView = new Main();
            var mainModel = new MainModel();
            var mainPresenter = new MainPresenter(mainView, mainModel);
            CMD.InitPresenter(mainPresenter);

            Application.Run(new AdminAplicationContext(mainView, ipportView, credentialsView));
        }
    }
}
