using System;
using System.Net.Sockets;
using System.IO;
using Shared;

namespace TcpChat
{
    internal sealed class TcpChatUI
    {
        private const string filePath = @"C:\Users\A217993\Desktop\TcpChatMessage";
        private const string ipAddress = "127.0.0.1";
        private const int port = 9999;

        public void CommunicateWithServer()
        { 
            var userMessage = string.Empty;
            var (socket, ipEndPoint) = SocketHandler.CreateSocket(ipAddress, port);

            socket.Connect(ipEndPoint);
            Logger.WriteToLog("Client connected to socket on " + ipEndPoint.Address + ":" + ipEndPoint.Port);

            while (userMessage.ToLower() != "/afk")
            {
                userMessage = GetUserInput();
                WriteMessageContent(userMessage, filePath);
                socket.SendFile(filePath);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Logger.PublishLog();
        }

        private static void WriteMessageContent(string userMessage, string filePath)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(userMessage);
                streamWriter.Close();
            }

            Logger.WriteToLog(userMessage);
        }

        private string GetUserInput()
        {
            Console.WriteLine("Type in a message you want to send to a friend!");
            return Console.ReadLine();
        }
    }
}
