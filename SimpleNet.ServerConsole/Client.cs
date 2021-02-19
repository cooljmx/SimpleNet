using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class Client : IDisposable
    {
        private readonly Socket _socket;

        public Client()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task StartAsync()
        {
            _socket.Connect(IPAddress.Parse("127.0.0.1"), 3000);

            if (_socket.Connected)
            {
                var networkStream = new NetworkStream(_socket);
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
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Disconnect(false);
            _socket.Dispose();
        }
    }
}