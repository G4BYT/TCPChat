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
                List<Socket> socketList = new List<Socket>(server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        server.clients[sock].timerAway.Enabled = false;
                        server.cmd.SendCommand(server.clients[sock], "Disconnect|");
                        server.cmd.SendToAllThisCommand("UserKicked|" + server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserKicked|" + server.clients[sock].nickname);
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            server.clients[sock].nickname + " has been kicked");
                        removeList.Add(sock);
                    }
                }
                foreach (Socket sock in removeList)
                    if (server.clients.ContainsKey(sock))
                        server.clients.Remove(sock);
            }
        }

        public static void Mute(List<IPEndPoint> ipEndPointList)
        {
            for(int i = 0; i < ipEndPointList.Count; i++)
            {
                List<Socket> socketList = new List<Socket>(server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        if (server.clients[sock].Muted == true)
                            continue;
                        server.clients[sock].Muted = true;
                        server.clients[sock].ChatIcon = "red";
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            server.clients[sock].nickname + " has been muted");
                        server.cmd.SendCommand(server.clients[sock], "Mute|");
                        server.cmd.SendToAllThisCommand("UserMuted|" + server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserMuted|" + server.clients[sock].nickname);
                    }
                }
            }
        }

        public static void UnMute(List<IPEndPoint> ipEndPointList)
        {
            for (int i = 0; i < ipEndPointList.Count; i++)
            {
                List<Socket> socketList = new List<Socket>(server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (sock.RemoteEndPoint.Equals(ipEndPointList[i]))
                    {
                        if (server.clients[sock].Muted == false)
                            continue;
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            server.clients[sock].nickname + " has been unmuted");
                        server.clients[sock].Muted = false;
                        server.clients[sock].ChatIcon = "green";
                        server.clients[sock].TimerReset();
                        server.cmd.SendCommand(server.clients[sock], "Unmute|");
                        server.cmd.SendToAllThisCommand("UserUnmuted|" + server.clients[sock].nickname, 
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("UserUnmuted|" + server.clients[sock].nickname);
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
                List<Socket> socketList = new List<Socket>(server.clients.Keys);
                for (int j = 0; j < socketList.Count; j++)
                {
                    Socket sock = socketList[j];
                    if (ipList[i].Equals(server.clients[sock].IPAddress))
                    {
                        server.Write(Server.Notification.Asterisk, sock.RemoteEndPoint.ToString() + " " +
                            server.clients[sock].nickname + " has been banned");
                        server.cmd.SendCommand(server.clients[sock], "Ban|");
                        server.cmd.SendToAllThisCommand("Banned|" + server.clients[sock].nickname,
                            new List<Socket> { sock });
                        AdminCMD.SendToAllThisCommand("Banned|" + server.clients[sock].nickname);
                        removeSock.Add(sock);

                    }
                }
                foreach (Socket sock in removeSock)
                {
                    server.clients.Remove(sock);
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
