using Shared;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpChat
{
    internal static class ClientSocketListener
    {
        private static Socket _clientSocket = new Socket(
            AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int _port = 9999;

        public static void CommunicateWithServer()
        {
            LoopConnect();
            SendLoop();
            Console.ReadLine();
        }

        private static void SendLoop()
        {
            while (true)
            {
                var request = GetUserInput();
                if (request.ToLower() == "/afk")
                {
                    Console.WriteLine("Disconnecting from server");
                    Logger.WriteToLog("Client has disconnected from the server");
                    break;
                }

                var requestToServer = Encoding.ASCII.GetBytes(request);
                _clientSocket.Send(requestToServer);

                var buffer = new byte[1024];
                var response = _clientSocket.Receive(buffer);
                var data = new byte[response];
                Array.Copy(buffer, data, response);
                Console.WriteLine("Received " + Encoding.ASCII.GetString(data));
            }
        }

        private static void LoopConnect()
        {
            var attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(IPAddress.Loopback, _port);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connection attempts: " + attempts);
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");
        }

        private static string GetUserInput()
        {
            Console.WriteLine("Input: ");
            var userInput = Console.ReadLine();

            Logger.WriteToLog("Client message: " + userInput);

            return userInput;
        }
    }
}
