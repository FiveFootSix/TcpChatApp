using Shared;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace TcpChat
{
    //  display who sent which message
    internal static class TcpClientUI
    {
        private static string _ipAddress = IPAddress.Loopback.ToString();
        private const int _port = 9999;

        public static void CommunicateWithServer()
        {
            var request = string.Empty;
            var tcpClient = new TcpClient(_ipAddress, _port);
            
            var thread = new Thread(Read);
            thread.Start(tcpClient);

            var streamWriter = new StreamWriter(tcpClient.GetStream());

            while (true)
            {
                if (tcpClient.Connected)
                {
                    request = GetUserRequest();
                    if (request.ToLower() == "/afk")
                    {
                        Console.WriteLine("Disconnecting from server");
                        Logger.WriteToLog("Client has disconnected from the server");
                        break;
                    }

                    streamWriter.WriteLine(request);
                    streamWriter.Flush();
                }
            }
        }

        private static void Read(object obj)
        {
            var tcpClient = (TcpClient)obj;
            var streamReader = new StreamReader(tcpClient.GetStream());

            while (true)
            {
                var message = streamReader.ReadLine();
                Console.WriteLine("not you: " + message);
            }
        }

        private static string GetUserRequest()
        {
            var userMessage = Console.ReadLine();

            Logger.WriteToLog("Client sent: " + userMessage);

            return userMessage;
        }
    }
}
