using System;
using System.Diagnostics;
using TcpChat;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Server is listening on port 9999...");

                var stopwatch = new Stopwatch();

                stopwatch.Start();
                while (stopwatch.Elapsed.TotalMinutes < 60)
                {
                    ServerSocketListener.StartListening();
                }

                stopwatch.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
