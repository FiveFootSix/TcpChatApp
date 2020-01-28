using Shared;
using System;

namespace TcpChat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClientSocketListener.CommunicateWithServer();
                Logger.PublishLog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
