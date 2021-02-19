using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    internal class NetworkStreamWrapper : INetworkStreamWrapper
    {
        private readonly NetworkStream _networkStream;

        public NetworkStreamWrapper(Socket socket)
        {
            _networkStream = new NetworkStream(socket);
        }

        public void Dispose()
        {
            _networkStream.Dispose();
        }

        public bool DataAvailable => _networkStream.DataAvailable;

        public ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
        {
            return _networkStream.ReadAsync(buffer, cancellationToken);
        }

        public ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
        {
            return _networkStream.WriteAsync(buffer, cancellationToken);
        }

        public int Read(Span<byte> buffer)
        {
            return _networkStream.Read(buffer);
        }

        public void Write(ReadOnlySpan<byte> buffer)
        {
            _networkStream.Write(buffer);
        }
    }
}