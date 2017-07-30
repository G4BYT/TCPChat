using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCLI
{
    public class Listener
    {
        Socket socket;
        Server server;

        public bool Listening{ get; private set;}

        public int Port { get; private set; }

        public delegate void SocketAcceptedHandler(Socket s);
        public event SocketAcceptedHandler SocketAccepted;

        public Listener(int port, Server server)
        {
            this.server = server;
            Port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening)
                return;
            try
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            }
            catch
            {
                server.Write(Server.Notification.Minus, "Port " + Port + " already used by another aplication");
                throw new SocketException();
            };
            socket.Listen(0);

            socket.BeginAccept(callback, null);
            Listening = true;
        }

        public void Stop()
        {
            if (!Listening)
                return;
            socket.Close();
            socket.Dispose();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void callback(IAsyncResult ar)
        {
            try
            {
                socket.BeginAccept(callback, null);
                Socket s = socket.EndAccept(ar);
                SocketAccepted?.Invoke(s);
            }
            catch
            {

            }
        }

        public static int GetPort(Server.AccesLevel accesLevel)
        {
            int port;
            while (true)
            {
                if (accesLevel == Server.AccesLevel.Client)
                    Console.Write("Enter the port for clients: ");
                else if (accesLevel == Server.AccesLevel.Admin)
                    Console.Write("Enter the port for admin: ");
                string holder = Console.ReadLine();
                bool isInt = int.TryParse(holder, out port);
                if (isInt)
                    if (IPEndPoint.MinPort <= port && IPEndPoint.MaxPort >= port)
                        return port;
                Console.WriteLine("Invalid port! Range " + IPEndPoint.MinPort + " - " + IPEndPoint.MaxPort);
            }
        }
    }
}
