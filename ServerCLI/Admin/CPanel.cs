using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ServerCLI
{
    class CPanel
    {
        static Server server;

        public static void Init(Server server)
        {
            CPanel.server = server;
        }


        public static void Kick(List<IPEndPoint> ipEndPointList)
        {
            for(int i = 0; i < ipEndPointList.Count; i++)
            {
                List<Socket> removeList = new List<Socket>();
                List<Socket> socketList = new List<Socket>(Server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        Server.clients[sock].timerAway.Enabled = false;
                        server.cmd.SendCommand(Server.clients[sock], "Disconnect|");
                        server.cmd.SendToAllThisCommand("UserKicked|" + Server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserKicked|" + Server.clients[sock].nickname);
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            Server.clients[sock].nickname + " has been kicked");
                        removeList.Add(sock);
                    }
                }
                foreach (Socket sock in removeList)
                    if (Server.clients.ContainsKey(sock))
                        Server.clients.Remove(sock);
            }
        }

        public static void Mute(List<IPEndPoint> ipEndPointList)
        {
            for(int i = 0; i < ipEndPointList.Count; i++)
            {
                List<Socket> socketList = new List<Socket>(Server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        if (Server.clients[sock].Muted == true)
                            continue;
                        Server.clients[sock].Muted = true;
                        Server.clients[sock].ChatIcon = "red";
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            Server.clients[sock].nickname + " has been muted");
                        server.cmd.SendCommand(Server.clients[sock], "Mute|");
                        server.cmd.SendToAllThisCommand("UserMuted|" + Server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserMuted|" + Server.clients[sock].nickname);
                    }
                }
            }
        }

        public static void UnMute(List<IPEndPoint> ipEndPointList)
        {
            for (int i = 0; i < ipEndPointList.Count; i++)
            {
                List<Socket> socketList = new List<Socket>(Server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        if (Server.clients[sock].Muted == false)
                            continue;
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            Server.clients[sock].nickname + " has been unmuted");
                        Server.clients[sock].Muted = false;
                        Server.clients[sock].ChatIcon = "green";
                        Server.clients[sock].TimerReset();
                        server.cmd.SendCommand(Server.clients[sock], "Unmute|");
                        server.cmd.SendToAllThisCommand("UserUnmuted|" + Server.clients[sock].nickname, 
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserUnmuted|" + Server.clients[sock].nickname);
                    }
                }
            }
        }

        public static void Ban(List<IPAddress> ipList)
        {
            string banIP = "BanIP";
            for (int i = 0; i < ipList.Count; i+=2)
            {
                if (!server.banMap.ContainsKey(ipList[i]))
                {
                    server.banMap.Add(ipList[i], null);
                    banIP += "|" + ipList[i];
                }
                List<Socket> removeSock = new List<Socket>();
                List<Socket> socketList = new List<Socket>(Server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (ipList[i].Equals(Server.clients[sock].IPAddress))
                    {
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            Server.clients[sock].nickname + " has been banned");
                        server.cmd.SendCommand(Server.clients[sock], "Ban|");
                        server.cmd.SendToAllThisCommand("Banned|" + Server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("Banned|" + Server.clients[sock].nickname);
                        removeSock.Add(sock);

                    }
                }
                foreach (Socket sock in removeSock)
                {
                    Server.clients.Remove(sock);
                    sock.Close();
                    sock.Dispose();
                }
                AdminCMD.SendToAllThisCommand(banIP);
            }
        }

        public static void UnBan(List<IPAddress> ipList)
        {
            for (int i = 0; i < ipList.Count; i++)
            {
                if (server.banMap.ContainsKey(ipList[i]))
                {
                    server.banMap.Remove(ipList[i]);
                    server.Write(Server.Notification.Asterisk, ipList[i].ToString() + " has been unbanned");
                }
            }
        }
    }
}
