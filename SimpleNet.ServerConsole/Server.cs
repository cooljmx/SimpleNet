using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class Server : IDisposable
    {
        private readonly Socket _socket;
        private bool _isStopped;

        public Server()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 3000));
            _socket.Listen(2);
        }

        private async Task AcceptLoopAsync()
        {
            do
            {
                var clientSocket = await _socket.AcceptAsync();
                var processClientThread = new Thread(async () => await ProcessClientAsync(clientSocket));
                processClientThread.Start();
            } while (!_isStopped);
        }

        private async Task ProcessClientAsync(Socket clientSocket)
        {
            using (clientSocket)
            {
                await using var networkStream = new NetworkStream(clientSocket);
                var networkStreamReader = new NetworkStreamReader(networkStream);
                var networkStreamWriter = new NetworkStreamWriter(networkStream);

                do
                {
                    if (networkStream.DataAvailable)
                    {
                        var buffer = await networkStreamReader.ReadAsync();

                        await networkStreamWriter.WriteAsync(buffer);
                    }
                } while (!_isStopped);
            }
        }

        public void Start()
        {
            _isStopped = false;

            var acceptThread = new Thread(async () => await AcceptLoopAsync());
            acceptThread.Start();
        }

        public void Dispose()
        {
            _isStopped = true;

            _socket.Close();
            _socket.Dispose();
        }
    }
}