using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Client.Model;

namespace Client
{
    public static class Client
    {
        public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string nickname;
        public static bool muted;

        public delegate void ReceivedHandler( byte[] data);
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
                    CMD.SendCommand("CheckNickname|" + nickname);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Server not responding", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }));
            if(connected == true)
            {
                Received += MainModel.Client_Received;
                ServerNotResponding += MainModel.Client_ServerNotResponding;
                socket.BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, callback, null);
            }
            return connected;
        }

        public static void SendNickname()
        {
            CMD.SendCommand("CheckNickname|" + nickname);
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
                if (ConnectModel.Valid.Equals("Valid"))
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
