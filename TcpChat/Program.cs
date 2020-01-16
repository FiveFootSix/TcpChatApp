using System;
using System.Net.Sockets;

namespace TcpChat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new TcpChatUI().CommunicateWithServer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
