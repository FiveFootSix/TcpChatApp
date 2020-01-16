using System.Text;
using Shared;

namespace Server
{
    internal static class ServerSocketListener
    {
        private static int _bufferSize = 1024;
        private static string _data;
        private static string ipAddress = "127.0.0.1";
        private static int port = 9999;

        public static void StartListening()
        {
            var bytes = new byte[_bufferSize];

            var (socket, ipEndPoint) = SocketHandler.CreateSocket(ipAddress, port);

            socket.Bind(ipEndPoint);
            socket.Listen(10);

            while (true)
            {
                var handler = socket.Accept();
                _data = null;

                while (true)
                {
                    var bytesReceived = handler.Receive(bytes);
                    _data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                    if (_data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                var message = Encoding.ASCII.GetBytes(_data);

                handler.Send(message);
            }
        }
    }
}
