using System;
using System.Text;
using System.Threading;

namespace ServerCLI
{

    class Program
    {
        static Server serv;
        static TerminalCommands terminal;
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            TerminalCommands.WriteBanner();
#if DEBUG
            int port = 3500;
            int adminPort = 3501;
            Admin.Username = "gabi";
            Admin.Password = "gabi";
#else
            int port = Listener.GetPort(Server.AccesLevel.Client);
            int adminPort = Listener.GetPort(Server.AccesLevel.Admin);
            Admin.GetUsernamePassword();
#endif
            serv = new Server(port, adminPort);
            terminal = new TerminalCommands(serv);
            Thread terminalThread = new Thread(terminal.GetCommand);
            terminalThread.Start();
        }
    }
}
