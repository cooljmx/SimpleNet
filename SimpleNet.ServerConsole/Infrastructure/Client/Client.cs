using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using SimpleNet.ServerConsole.Infrastructure.Common;

namespace SimpleNet.ServerConsole.Infrastructure.Client
{
    public class Client : IDisposable
    {
        private readonly ISocketWrapper _socketWrapper;

        public Client()
        {
            _socketWrapper = new SocketWrapper();
        }

        public async Task StartAsync()
        {
            _socketWrapper.Connect("127.0.0.1", 3000);

            if (_socketWrapper.Connected)
            {
                var networkStream = _socketWrapper.CreateNetworkStream();
                var networkStreamReader = new NetworkStreamReader(networkStream);
                var networkStreamWriter = new NetworkStreamWriter(networkStream);

                var length = 123456;
                var lengthBuffer = BitConverter.GetBytes(length);

                await networkStreamWriter.WriteAsync(lengthBuffer);
                var buffer = await networkStreamReader.ReadAsync();

                var actualLength = BitConverter.ToUInt32(buffer);
                Console.WriteLine(actualLength);
            }
        }

        public void Dispose()
        {
            _socketWrapper.Shutdown(SocketShutdown.Both);
            _socketWrapper.Disconnect();
            _socketWrapper.Dispose();
        }
    }
}