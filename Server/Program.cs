using System;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpServer.StartListening();
                Console.ReadLine();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
