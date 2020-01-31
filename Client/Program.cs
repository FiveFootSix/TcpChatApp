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
                TcpClientUI.CommunicateWithServer();
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
