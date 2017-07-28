using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace ServerCLI
{
    public class Client
    {
        public Socket socket;

        public IPEndPoint IpEndPoint { get; private set; }
        public string nickname { get; set; }
        public Server server;
        private readonly int TimerInterval = 6000;
        public bool Muted { get; set; }
        public string ChatIcon = "green";
        public DateTime LastMessageTime { get; set; }
        public string LastMessage { get; set; }
        public bool Away { get; set; } = false;
        public IPAddress IPAddress
        {
            get
            {
                string ipString = IpEndPoint.ToString();
                ipString = ipString.Remove(ipString.IndexOf(':'), ipString.Length - ipString.IndexOf(':'));
                return IPAddress.Parse(ipString);
            }
        }


        public Timer timerAway = new Timer();

        public delegate void ReceivedHandler(Client sender, byte[] data);
        public delegate void DisconnectedHandler(Client sender);

        public event ReceivedHandler Received;
        public event DisconnectedHandler Disconnected;

        public Client(Socket accepted, Server server)
        {
            this.server = server;
            timerAway.Interval = TimerInterval;
            timerAway.Elapsed += TimerAway_Tick;
            timerAway.Enabled = true;
            timerAway.Start();
            socket = accepted;
            IpEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            socket.BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, callback, null);
        }

        private void callback(IAsyncResult ar)
        {
            try
            {
                socket.EndReceive(ar);
                byte[] buffer = new byte[socket.ReceiveBufferSize];

                int size = socket.Receive(buffer, buffer.Length, 0);
                if(size < buffer.Length)
                {
                    Array.Resize(ref buffer, size);
                }
                Received?.Invoke(this, buffer);
                socket.BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, callback, null);
            }
            catch
            {
                Close();
                Disconnected?.Invoke(this);
            }
        }

        private void TimerAway_Tick(object sender, EventArgs e)
        {
            if (nickname == null)
            {
                    timerAway.Stop();
                    timerAway.Interval = TimerInterval;
                    timerAway.Start();
                    return;
            }

            if (ChatIcon == "green")
            {
                ChatIcon = "yellow";
                server.cmd.SendToAllThisCommand("UserIdle|" + nickname);
                AdminCMD.SendToAllThisCommand("UserIdle|" + nickname);
            }
            else
                timerAway.Stop();
        }

        public void TimerReset()
        {
            if (ChatIcon == "yellow")
            {
                ChatIcon = "green";
                server.cmd.SendToAllThisCommand("UserActive|" + nickname);
                AdminCMD.SendToAllThisCommand("UserActive|" + nickname);
            }
            timerAway.Stop();
            timerAway.Interval = TimerInterval;
            timerAway.Start();
        }

        public void Close()
        {
            socket.Close();
            socket.Dispose();
        }

    }
}
