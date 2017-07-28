using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerCLI
{
    class Admin
    {
        private static Server _server;
        public static Server Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
                AdminCMD.server = value;
            }
        }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static List<Admin> adminList = new List<Admin>();
        public bool UsernameCorrect { get; set; } = false;
        public bool PasswordCorrect { get; set; } = false;
        public Socket socket;
        public IPEndPoint IpEndPoint { get; private set; }

        public delegate void ReceivedHandler(Admin admin, byte[] data);
        public delegate void DisconnectedHandler(Admin admin);

        public event ReceivedHandler Received;
        public event DisconnectedHandler Disconnected;

        public Admin(Socket socket)
        {
            this.socket = socket;
            this.IpEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, 0);
        }

        private void callback(IAsyncResult ar)
        {
            try
            {
                socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, 0);
                socket.EndReceive(ar);
                byte[] buffer = new byte[socket.ReceiveBufferSize];

                int size = socket.Receive(buffer, buffer.Length, 0);
                if (size < buffer.Length)
                {
                    Array.Resize(ref buffer, size);
                }
                Received?.Invoke(this, buffer);
            }
            catch
            {
                Close();
                Disconnected?.Invoke(this);
            }
        }

        public void Admin_Received(Admin admin, byte[] data)
        {
            Task.Factory.StartNew(() =>
            {
                if (adminList.Contains(admin))
                {
                    string encStr = Encoding.UTF8.GetString(data);
                    AdminCMD.ReceiveCommand(admin, Cryptography.Decrypt(encStr, Cryptography.Target.Admin));
                }
            });
        }

        public static void GetUsernamePassword()
        {
            string str;
            Console.Write("Enter the username for admin: ");
            str = Console.ReadLine();
            Username = str;
            Console.Write("Enter the password for admin: ");
            str = Console.ReadLine();
            Password = str;
        }

        public void Admin_Disconnected(Admin admin)
        {
            if (adminList.Contains(admin))
                adminList.Remove(admin);
        }

        public void Close()
        {
            socket.Close();
            socket.Dispose();
        }
    }
}
