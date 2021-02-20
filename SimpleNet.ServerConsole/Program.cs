using System;

namespace SimpleNet.ServerConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "client")
            {
                for (var i = 0; i < 100; i++)
                {
                    using var client = new Client();
                    client.StartAsync().GetAwaiter().GetResult();
                }
            }
            else
            {
                using var server = new ServerListener();
                server.Start();

                Console.ReadKey();
            }
        }
    }
}