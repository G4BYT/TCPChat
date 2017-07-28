using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace ServerCLI
{
    class TerminalCommands
    {
        Server server;
        public TerminalCommands(Server server)
        {
            this.server = server;
            CPanel.Init(server);
        }

        public void InterpretCommand(string command)
        {
            string[] commands = command.Split(' ');
            List<Socket> socketList = new List<Socket>(server.clients.Keys);
            List<IPAddress> ipList;
            List<IPEndPoint> ipEndPointList;
            IPAddress ip = null;
            IPEndPoint ipEndPoint = null;
            int port;
            switch (commands[0])
            {
                case "clear":
                    WriteBanner();
                    break;

                case "show":
                    switch (commands[1])
                    {
                        case "clients":
                            if (server.clients.Count == 0)
                            {
                                server.Write(Server.Notification.Asterisk, "No clients connected to server");
                                return;
                            }
                            for (int i = 0; i < socketList.Count; i++)
                            {
                                server.Write(Server.Notification.Asterisk,
                                    socketList[i].RemoteEndPoint + " " + server.clients[socketList[i]].nickname);
                            }
                            break;

                        case "banlist":
                            if(server.banMap.Count == 0)
                            {
                                server.Write(Server.Notification.Asterisk, "No ip's found in ban list");
                                return;
                            }
                            foreach(IPAddress IP in server.banMap.Keys)
                            {
                                server.Write(Server.Notification.Asterisk, IP.ToString());
                            }
                            break;

                        case "info":
                            if (server.clients.Count == 0)
                            {
                                server.Write(Server.Notification.Asterisk, "No clients connected to server");
                                return;
                            }
                            if (commands.Length < 3)
                                return;
                            for(int i = 2; i < commands.Length; i++)
                            {
                                for (int j = 0; j < socketList.Count; j++)
                                {
                                    if (socketList[j].RemoteEndPoint.ToString().Equals(commands[i]))
                                    {
                                        server.Write(Server.Notification.Asterisk, socketList[j].RemoteEndPoint.ToString());
                                        server.Write(Server.Notification.Asterisk, "Nickname: "+ server.clients[socketList[j]].nickname);
                                        server.Write(Server.Notification.Asterisk, "Muted: " + server.clients[socketList[j]].Muted.ToString());
                                        server.Write(Server.Notification.Asterisk, "Away: " + server.clients[socketList[j]].Away.ToString());
                                        server.Write(Server.Notification.Asterisk, "Last Message: " + server.clients[socketList[j]].LastMessage);
                                        server.Write(Server.Notification.Asterisk, "Last Time Message: " + server.clients[socketList[j]].LastMessageTime.ToLongTimeString());
                                    }
                                }

                            }
                            break;

                        case "admins":
                            if (Admin.adminList.Count == 0)
                            {
                                server.Write(Server.Notification.Asterisk, "There are no administrators connected to the server");
                                return;
                            }
                            for(int i = 0; i < Admin.adminList.Count; i++)
                            {
                                server.Write(Server.Notification.Asterisk, Admin.adminList[i].IpEndPoint.ToString());
                            }
                            break;
              

                    }
                    break;

                case "kick":
                    ipEndPointList = new List<IPEndPoint>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        try
                        {
                            ip = IPAddress.Parse(commands[i].Remove(commands[i].IndexOf(':'),
                                commands[i].Length - commands[i].IndexOf(':')));
                            port = int.Parse(commands[i].Substring(commands[i].IndexOf(':') + 1));
                            ipEndPoint = new IPEndPoint(ip, port);
                        }
                        catch
                        {

                        }
                        if(ipEndPoint != null)
                        ipEndPointList.Add(ipEndPoint);
                    }
                    if (ipEndPointList != null)
                        CPanel.Kick(ipEndPointList);
                    break;

                case "mute":
                    ipEndPointList = new List<IPEndPoint>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        try
                        {
                            ip = IPAddress.Parse(commands[i].Remove(commands[i].IndexOf(':'),
                                commands[i].Length - commands[i].IndexOf(':')));
                            port = int.Parse(commands[i].Substring(commands[i].IndexOf(':') + 1));
                            ipEndPoint = new IPEndPoint(ip, port);
                        }
                        catch
                        {

                        }
                        if (ipEndPoint != null)
                            ipEndPointList.Add(ipEndPoint);
                    }
                    if (ipEndPointList != null)
                        CPanel.Mute(ipEndPointList);
                    break;

                case "unmute":
                    ipEndPointList = new List<IPEndPoint>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        try
                        {
                            ip = IPAddress.Parse(commands[i].Remove(commands[i].IndexOf(':'),
                                commands[i].Length - commands[i].IndexOf(':')));
                            port = int.Parse(commands[i].Substring(commands[i].IndexOf(':') + 1));
                            ipEndPoint = new IPEndPoint(ip, port);
                        }
                        catch
                        {

                        }
                        if(ipEndPoint != null)
                        ipEndPointList.Add(ipEndPoint);
                    }
                    if (ipEndPointList != null)
                        CPanel.UnMute(ipEndPointList);
                    break;


                case "ban":
                    ipList = new List<IPAddress>();
                    for (int i = 1; i < commands.Length; i++)
                    {
                        try
                        {
                            ip = IPAddress.Parse(commands[i]);
                        }
                        catch
                        {

                        }
                        if(ip != null)
                        ipList.Add(IPAddress.Parse(commands[i]));
                    }
                    if (ipList != null)
                        CPanel.Ban(ipList);
                    break;

                case "unban":
                    ipList = new List<IPAddress>();
                    for(int i = 1; i < commands.Length; i++)
                        ipList.Add(IPAddress.Parse(commands[i]));
                    CPanel.UnBan(ipList);
                    break;
                
            }
        }

        static string[] banner = new string[]
        {
            @"  _______ _____ _____     _____ _    _       _______ ",
            @" |__   __/ ____|  __ \   / ____| |  | |   /\|__   __|",
            @"    | | | |    | |__) | | |    | |__| |  /  \  | |   ",
            @"    | | | |    |  ___/  | |    |  __  | / /\ \ | |   ",
            @"    | | | |____| |      | |____| |  | |/ ____ \| |   ",
            @"    |_|  \_____|_|       \_____|_|  |_/_/    \_\_|   ",
            @"                                                     ",
            @"                                                     "
        };

        static public void WriteBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < banner.Length; i++)
            {
                Console.WriteLine(banner[i]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void GetCommand()
        {
            string command;
            for (;;)
            {
                command = Console.ReadLine();
                InterpretCommand(command);
            }
        }
    }
}
