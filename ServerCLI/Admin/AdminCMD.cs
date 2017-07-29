using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ServerCLI
{
    class AdminCMD
    {
        public static Server server;

        public static void ReceiveCommand(Admin admin, string command)
        {
            string[] commands = command.Split('|');
            List<IPEndPoint> ipEndPointList;
            List<IPAddress> ipAddresList;
            switch(commands[0])
            {
                case "CheckUsernamePassword":
                    if (commands[1] == Admin.Username && commands[2] == Admin.Password)
                    {
                        admin.UsernameCorrect = true;
                        admin.PasswordCorrect = true;
                        string connectCommand;
                        connectCommand = "UsernamePasswordAccepted";
                        if (!Admin.adminList.Contains(admin))
                            return;
                        List<Socket> socketList = new List<Socket>(Server.clients.Keys);
                        for (int i = 0; i < socketList.Count; i++)
                        {
                            Client client = Server.clients[socketList[i]];
                            connectCommand += "|" + client.nickname
                                + "|" + client.ChatIcon + "|" + client.IpEndPoint.ToString() + "|" +
                                client.LastMessageTime.ToLongTimeString() + "|" + client.LastMessage;
                        }
                        AdminCMD.SendCommand(admin, connectCommand);
                        if (server.banMap.Count == 0)
                            return;
                        string str = "BanIP";
                        foreach(IPAddress ip in server.banMap.Keys)
                        {
                            str += "|" + ip.ToString() + "|" + server.banMap[ip]?.ToLongTimeString();
                        }
                        AdminCMD.SendCommand(admin, str);
                    }
                    else
                    {
                        admin.UsernameCorrect = false;
                        admin.PasswordCorrect = false;
                        SendCommand(admin, "UsernamePasswordRejected|");
                    }
                    break;

                case "Kick":
                    if (!Admin.adminList.Contains(admin) || !admin.UsernameCorrect || !admin.PasswordCorrect)
                        return;
                    ipEndPointList = new List<IPEndPoint>();
                    for(int i = 1; i < commands.Length; i++)
                    {
                        ipEndPointList.Add(ReturnIPEndPoint(commands[i]));
                    }
                    CPanel.Kick(ipEndPointList);
                    break;

                case "Mute":
                    if (!Admin.adminList.Contains(admin) || !admin.UsernameCorrect || !admin.PasswordCorrect)
                        return;
                    ipEndPointList = new List<IPEndPoint>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        ipEndPointList.Add(ReturnIPEndPoint(commands[i]));
                    }
                    CPanel.Mute(ipEndPointList);
                    break;

                case "UnMute":
                    if (!Admin.adminList.Contains(admin) || !admin.UsernameCorrect || !admin.PasswordCorrect)
                        return;
                    ipEndPointList = new List<IPEndPoint>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        ipEndPointList.Add(ReturnIPEndPoint(commands[i]));
                    }
                    CPanel.UnMute(ipEndPointList);
                    break;

                case "Ban":
                    if (!Admin.adminList.Contains(admin) || !admin.UsernameCorrect || !admin.PasswordCorrect)
                        return;
                    ipAddresList = new List<IPAddress>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        ipAddresList.Add(IPAddress.Parse(commands[i]));
                    }
                    CPanel.Ban(ipAddresList);
                    break;

                case "UnBan":
                    string unBan = "UnBan";
                    if (!Admin.adminList.Contains(admin) || !admin.UsernameCorrect || !admin.PasswordCorrect)
                        return;
                    ipAddresList = new List<IPAddress>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        ipAddresList.Add(IPAddress.Parse(commands[i]));
                        unBan += "|" + commands[i];
                    }
                    CPanel.UnBan(ipAddresList);
                    AdminCMD.SendToAllThisCommand(unBan);
                    break;

                case "ServerGlobalMessage":
                    server.cmd.SendToAllThisCommand("ServerGlobalMessage|" + commands[1]);
                    break;

            }
        }

        public static void SendCommand(Admin admin, string command)
        {
            Monitor.Enter(admin.socket);
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Admin);
            try
            {
                if (Admin.adminList.Contains(admin))
                {
                    admin.socket.Send(buffer, 0, buffer.Length, 0);
                }
            }
            catch
            {

            }
            finally
            {
                Monitor.Exit(admin.socket);
            }
        }

        public static void SendToAllThisCommand(string command)
        {
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Admin);
            for (int i = 0; i < Admin.adminList.Count; i++)
            {
                try
                {
                    Monitor.Enter(Admin.adminList[i].socket);
                    Admin.adminList[i].socket.Send(buffer, 0, buffer.Length, 0);
                }
                catch
                {

                }
                finally
                {
                    Monitor.Exit(Admin.adminList[i].socket);
                }
            }
        }

        public static IPEndPoint ReturnIPEndPoint(string ipport)
        {
            IPAddress ip = IPAddress.Parse(ipport.Remove(ipport.IndexOf(':'),
                           ipport.Length - ipport.IndexOf(':')));
            int port = int.Parse(ipport.Substring(ipport.IndexOf(':') + 1));
            return new IPEndPoint(ip, port);
        }
    }
}
