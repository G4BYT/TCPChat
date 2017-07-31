using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerCLI.Tests
{
    [TestClass()]
    public class ServerTests
    {
        /// <summary>
        /// This test simulates a bounch of clients that are trying to connect to the server.
        /// You need to do the following in order to use this test unit:
        /// 1. Open ServerCLI from release or debug folder. The server needs to run on the same local ip as the test unit.
        /// 2. If you choose release, set the port for users 3500.
        /// 3. Run the test
        /// </summary>
        [TestMethod()]
        public void Test()
        {
            // arrange
            List<Socket> socketList = new List<Socket>();
            int expectedOutput = 100;
            int succesfulConnected = 0;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int factoryStop = 0;
            int port = 3500;
            string connectString;
            Random rnd = new Random();

            // act
            for (int i = 0; i < expectedOutput; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    while (!socket.Connected)
                        try
                        {
                            socket.Connect(new IPEndPoint(ip, port));
                        }
                        catch
                        {

                        }
                    connectString = "CheckNickname|" + rnd.Next(100000000, 999999999);
                    socket.Send(Cryptography.Encrypt(connectString, Cryptography.Target.Client));
                    if (socket.Connected)
                    {
                        factoryStop++;
                        succesfulConnected++;
                    }
                    socketList.Add(socket);
                });
                Thread.Sleep(10);
            }
            while (factoryStop < expectedOutput)
                Thread.Sleep(1);

            // assert
            Assert.AreEqual(expectedOutput, succesfulConnected);
        }
    }
}