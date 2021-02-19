using System;

namespace SimpleNet.ServerConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "client")
            {
                using var client = new Client();
                client.StartAsync().GetAwaiter().GetResult();
            }
            else
            {
                using var server = new Server();
                server.Start();

                Console.ReadKey();
            }
        }
    }
}