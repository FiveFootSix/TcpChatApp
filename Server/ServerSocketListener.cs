using System.Text;
using System;
using System.Net.Sockets;
using System.Net;
using Shared;

namespace Server
{
    internal sealed class ServerSocketListener
    {
        private static byte[] _buffer = new byte[1024];
        private const int _port = 9999;
        private static Socket _serverSocket = new Socket(
            AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void StartListening()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Loopback, _port);

            _serverSocket.Bind(ipEndPoint);
            _serverSocket.Listen(10);

            Console.WriteLine("Server is listening on port " + _port);

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult asyncResult)
        {
            var socket = _serverSocket.EndAccept(asyncResult);

            Console.WriteLine("Client Connected");

            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult asyncResult)
        {
            var socket = (Socket)asyncResult.AsyncState;
            var received = socket.EndReceive(asyncResult);
            var data = new byte[received];

            Array.Copy(_buffer, data, received);

            var receivedMessage = Encoding.ASCII.GetString(data);
            Console.WriteLine(receivedMessage);
            Logger.WriteToLog("Message received from client: " + receivedMessage);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private static void SendCallback(IAsyncResult asyncResult)
        {
            var socket = (Socket)asyncResult.AsyncState;
            socket.EndSend(asyncResult);
        }
    }
}
