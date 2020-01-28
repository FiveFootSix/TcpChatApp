using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerSocketListener.StartListening();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
