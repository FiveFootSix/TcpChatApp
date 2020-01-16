using System.Net.Sockets;
using System.Net;

namespace Shared
{
    public static class SocketHandler
    {
        public static (Socket, IPEndPoint) CreateSocket(string ipAddress, int port)
        {
            var currentIpAddress = IPAddress.Parse(ipAddress);
            var ipEndPoint = new IPEndPoint(currentIpAddress, port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            return (socket, ipEndPoint);
        }
    }
}
