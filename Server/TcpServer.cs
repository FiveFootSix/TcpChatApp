using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    //  todo: dont let the server crash when a client disconnects
    //  todo: show which user is sending which message
    internal sealed class TcpServer
    {
        private const int _port = 9999;
        private static TcpListener _tcpListener;
        private static List<TcpClient> _tcpClientsList = new List<TcpClient>();

        public static void StartListening()
        {
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();

            Console.WriteLine("Server is listening on port " + _port);

            while (true)
            {
                var tcpClient = _tcpListener.AcceptTcpClient();
                _tcpClientsList.Add(tcpClient);

                var thread = new Thread(ClientListener);
                thread.Start(tcpClient);
            }
        }

        private static void ClientListener(object obj)
        {
            var tcpClient = (TcpClient)obj;
            var streamReader = new StreamReader(tcpClient.GetStream());

            Console.WriteLine("Client connected");

            while (true)
            {
                var message = streamReader.ReadLine();
                BroadCastClientMessage(message, tcpClient);
                Console.WriteLine(message);
            }
        }

        private static void BroadCastClientMessage(string message, TcpClient excludeClient)
        {
            foreach (var tcpClient in _tcpClientsList)
            {
                if (tcpClient != excludeClient)
                {
                    var streamWriter = new StreamWriter(tcpClient.GetStream());
                    streamWriter.WriteLine(message);
                    streamWriter.Flush();
                }
            }
        }
    }
}