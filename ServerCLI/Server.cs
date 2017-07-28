using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerCLI
{
    public class Server
    {
        static readonly Object obj = new Object();
        public Dictionary<Socket, Client> clients = new Dictionary<Socket, Client>();
        public Dictionary<IPAddress, DateTime?> banMap = new Dictionary<IPAddress, DateTime?>();
        public Listener listener;
        public Listener adminListener;
        public CMD cmd;

        public enum Notification
        {
            Plus,
            Minus,
            ExclamationMark,
            Asterisk
        }

        public enum AccesLevel
        {
            Client,
            Admin
        }

        public Server(int port, int adminPort)
        {
            listener = new Listener(port, this);
            listener.SocketAccepted += Listen_SocketAccepted;
            listener.Start();
            adminListener = new Listener(adminPort, this);
            Admin.Server = this;
            adminListener.SocketAccepted += Listen_SocketAcceptedAdmin;
            adminListener.Start();
            cmd = new CMD(this);
        }

        private void Listen_SocketAccepted(Socket s)
        {
            Task.Factory.StartNew(() =>
            {
                IPAddress senderIPAddress;
                string ipString = s.RemoteEndPoint.ToString();
                ipString = ipString.Remove(
                        ipString.IndexOf(':'), ipString.Length - ipString.IndexOf(':'));
                senderIPAddress = IPAddress.Parse(ipString);
                // Check if the ip address that tries to connect is banned from the server
                if (banMap.ContainsKey(senderIPAddress))
                {
                    cmd.SendCommand(s, "YouAreBanned|");
                    banMap[senderIPAddress] = DateTime.Now;
                    AdminCMD.SendToAllThisCommand("BanIP|" + senderIPAddress.ToString() + "|" + banMap[senderIPAddress]?.ToLongTimeString());
                    return;
                }
                // Check if the ip addres is from LAN
                if (ipString.Split('.')[0] == "192" || ipString.Split('.')[0] == "172"
                || ipString.Split('.')[0] == "10" || ipString.Split('.')[0] == "8")
                {
                    foreach (Socket sock in clients.Keys)
                    {
                        if (senderIPAddress.Equals(clients[sock].IPAddress))
                        {
                            cmd.SendCommand(s, "SameIPClient|");
                            return;
                        }
                    }
                }
                Client client = new Client(s, this);
                client.Received += Client_Received;
                client.Disconnected += Client_Disconnected;
                clients.Add(s, client);
                Write(Notification.ExclamationMark, client.IpEndPoint.ToString() + " is connecting to server.");
            });
        }

        private void Listen_SocketAcceptedAdmin(Socket s)
        {
            Task.Factory.StartNew(() =>
            {
                Admin admin = new Admin(s);
                admin.Received += admin.Admin_Received;
                admin.Disconnected += admin.Admin_Disconnected;
                Admin.adminList.Add(admin);

            });
        }

        private void Client_Received(Client sender, byte[] data)
        {
            Task.Factory.StartNew(() =>
            {
                if (clients.ContainsKey(sender.socket))
                {
                    string encStr = Encoding.UTF8.GetString(data);
                    cmd.ReceiveCommand(sender, Cryptography.Decrypt(encStr, Cryptography.Target.Client));
                }
            });
        }

        private void Client_Disconnected(Client sender)
        {
            if (clients.ContainsKey(sender.socket))
            {
                sender.timerAway.Stop();
                if (sender.nickname != " ")
                {
                    cmd.ReceiveCommand(sender, "Disconnected|" + sender.nickname);
                    AdminCMD.SendToAllThisCommand("Disconnected|" + sender.nickname);
                }
                clients.Remove(sender.socket);
            }
        }

        public void Write(Notification notif, string message)
        {
            Task.Factory.StartNew(() =>
            {
                Monitor.Enter(obj);
                switch (notif)
                {
                    case Notification.Plus:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" [");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("+");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("] ");
                        break;

                    case Notification.Minus:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" [");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("-");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("] ");
                        break;

                    case Notification.ExclamationMark:
                        Console.Write(" [!] ");
                        break;

                    case Notification.Asterisk:
                        Console.Write(" [*] ");
                        break;

                }
                Console.WriteLine(message);
                Monitor.Exit(obj);
            });

        }
    }
}