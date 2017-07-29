using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace ServerCLI
{
   public class CMD
    {
        private Server server;

        public CMD(Server server)
        {
            this.server = server;
        }

        public void ReceiveCommand(Client sender, string command)
        {
            string[] commands = command.Split('|');
            List<Socket> socketList = new List<Socket>(Server.clients.Keys);
            switch (commands[0])
            {
                case "CheckNickname":
                    server.Write(Server.Notification.ExclamationMark, sender.IpEndPoint.ToString() + " is trying the nickname " + commands[1]);
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if(socketList[i] != null && Server.clients.ContainsKey(socketList[i]))
                            if (Server.clients[socketList[i]]?.nickname?.ToLower() == commands[1].ToLower()
                                || commands[1].ToLower() == "server" || commands[1].ToLower() == "bot")
                            {
                                SendCommand(sender, "NicknameAlreadyUse|");
                                return;
                            }
                    }
                    SendCommand(sender, "NicknameAvailable|");
                    break;

                case "Connected":
                    string connectCommand;
                    if (Server.clients.ContainsKey(sender.socket))
                    {
                        sender.nickname = commands[1];
                        server.Write(Server.Notification.Plus, sender.IpEndPoint.ToString() + " has been connected with the nickname " + sender.nickname);
                    }
                    connectCommand = "Connected|" + sender.nickname + "|" + sender.ChatIcon;
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (socketList[i] == sender.socket)
                            continue;
                        connectCommand += "|" + Server.clients[socketList[i]].nickname
                            + "|" + Server.clients[socketList[i]].ChatIcon;
                    }
                    SendCommand(sender, connectCommand); 
                    connectCommand = "NewUser|" + sender.nickname + "|" + sender.ChatIcon;
                    AdminCMD.SendToAllThisCommand(connectCommand + "|" + sender.IpEndPoint + "|" +
                            sender.LastMessageTime.ToLongTimeString() + "|" + sender.LastMessage);
                    SendToAllThisCommand(connectCommand, new List<Socket> { sender.socket });
                    break;

                case "GlobalMessage":
                    if(sender.Muted == true)
                    {
                        SendCommand(sender, "Mute|");
                        return;
                    }
                    sender.TimerReset();
                    SendToAllThisCommand("GlobalMessage|" + commands[1] + "|" + commands[2]);
                    if (Server.clients.ContainsKey(sender.socket))
                    {
                        sender.LastMessageTime = DateTime.Now;
                        sender.LastMessage = commands[2];
                    }
                    AdminCMD.SendToAllThisCommand("GlobalMessage|" + commands[1] + "|" + commands[2] +"|" + sender.LastMessageTime.ToLongTimeString());
                    break;

                case "PrivateChat":
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (Server.clients[socketList[i]].nickname == commands[1])
                        {
                            SendCommand(Server.clients[socketList[i]], "VerifyPrivateChat|" + sender.nickname);
                        }
                    }
                    break;

                case "PrivateChatAccepted":
                    SendCommand(sender, "PrivateChatAccepted|" + commands[1]);
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (Server.clients[socketList[i]].nickname == commands[1])
                        {
                            SendCommand(Server.clients[socketList[i]], "PrivateChatAccepted|" + sender.nickname);
                        }
                    }
                    break;

                case "PrivateMessage":
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (Server.clients[socketList[i]].nickname == commands[1])
                        {
                            SendCommand(Server.clients[socketList[i]], "PrivateMessage|" + sender.nickname + 
                                "|" + commands[2] );
                        }
                    }
                    break;

                case "Blocked":
                    for (int i = 0; i < socketList.Count; i++)
                    {
                        if (Server.clients[socketList[i]].nickname == commands[1])
                        {
                            SendCommand(Server.clients[socketList[i]], "Blocked|" + sender.nickname);
                        }
                    }
                    break;

                case "Disconnected":
                    server.Write(Server.Notification.Minus, sender.IpEndPoint.ToString() + " " + sender.nickname + " has been disconnected");
                    SendToAllThisCommand("Disconnected|" + commands[1]);
                    break;
            }
        }

        public void SendCommand(Client sender, string command)
        {
            Monitor.Enter(sender.socket);
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Client);
            try
            {
                if (Server.clients.ContainsKey(sender.socket))
                {
                    sender.socket.Send(buffer, 0, buffer.Length, 0);
                }
            }
            catch
            {

            }
            finally
            {
                Monitor.Exit(sender.socket);
            }
        }

        public void SendCommand(Socket sender, string command)
        {
            Monitor.Enter(sender);
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Client);
            try
            {
                    sender.Send(buffer, 0, buffer.Length, 0);
            }
            catch
            {

            }
            finally
            {
                Monitor.Exit(sender);
            }
        }

        public void SendToAllThisCommand(string command)
        {
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Client);
            List<Socket> socketList =  new List<Socket>(Server.clients.Keys);
            for(int i = 0; i < socketList.Count; i++)
            {
                try
                {
                    Monitor.Enter(socketList[i]);
                    socketList[i].Send(buffer, 0, buffer.Length, 0);
                }
                catch
                {

                }
                finally
                {
                    if (socketList[i] != null)
                        Monitor.Exit(socketList[i]);
                }
            }
        }

        public void SendToAllThisCommand(string command, List<Socket> exclude)
        {
            byte[] buffer = Cryptography.Encrypt(command, Cryptography.Target.Client);
            List<Socket> socketList = new List<Socket>(Server.clients.Keys);
            for (int i = 0; i < socketList.Count; i++)
            {
                if (exclude.Contains(socketList[i]))
                    continue;
                try
                {
                    Monitor.Enter(socketList[i]);
                    socketList[i].Send(buffer, 0, buffer.Length, 0);
                }
                catch
                {

                }
                finally
                {
                    Monitor.Exit(socketList[i]);
                }
            }
        }
    }
}
