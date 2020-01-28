using System;
using System.Collections.Generic;
using System.IO;

namespace Shared
{
    public static class Logger
    {
        private static string _filePath = @"C:\Users\A217993\Desktop\TcpChatAppLog";
        private static readonly List<string> _log = new List<string>();

        public static void WriteToLog(string message)
        {
            _log.Add(message);
        }

        public static void PublishLog()
        {
            using (var streamWriter = new StreamWriter(_filePath))
            {
                foreach (var message in _log)
                {
                    streamWriter.WriteLine(message);
                }
                streamWriter.Close();
            }
        }
    }
}
