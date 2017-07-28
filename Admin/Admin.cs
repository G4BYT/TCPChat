using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin
{
    static class Admin
    {
        public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static bool Connected { get; set; } = false;

        public delegate void ReceivedHandler(byte[] data);
        public delegate void ServerNotRespondingHandler();

        public static event ReceivedHandler Received;
        public static event ServerNotRespondingHandler ServerNotResponding;

        public static async Task<bool> Connect(IPAddress ip, int port)
        {
            bool connected = await Task.Factory.StartNew(new Func<bool>(() =>
            {
                try
                {
                    socket.Connect(new IPEndPoint(ip, port));
                    Connected = true;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Server not responding", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connected = false;
                    return false;
                }
            }));
            if (connected == true)
            {
                Connected = true;
                Received += MainModel.Admin_Received;
                ServerNotResponding += MainModel.Admin_ServerNotResponding;
                socket.BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, callback, null);
            }
            else
                return false;
            return true;
        }

        public static void SendCredentials(string username, string password)
        {
            CMD.SendCommand(socket, "CheckUsernameNickname|" + username + "|" + password);
        }

        public static void callback(IAsyncResult ar)
        {
            try
            {
                socket.BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, callback, null);
                socket.EndReceive(ar);
                byte[] buffer = new byte[socket.ReceiveBufferSize];
                int size = socket.Receive(buffer, 0, buffer.Length, 0);
                if (size < buffer.Length)
                {
                    Array.Resize(ref buffer, size);
                }
                Received?.Invoke(buffer);
            }
            catch
            {
                if (CredentialsModel.ValidCredentials)
                {
                    ServerNotResponding?.Invoke();
                    Close();
                }
            }
        }

        public static void Close()
        {
            socket.Close();
            socket.Dispose();
        }

    }
}
