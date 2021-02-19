using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class NetworkStreamWriter
    {
        private readonly INetworkStreamWrapper _networkStreamWrapper;

        public NetworkStreamWriter(INetworkStreamWrapper networkStreamWrapper)
        {
            _networkStreamWrapper = networkStreamWrapper;
        }

        public async Task WriteAsync(ReadOnlyMemory<byte> buffer)
        {
            var lengthBuffer = BitConverter.GetBytes(buffer.Length);

            await _networkStreamWrapper.WriteAsync(lengthBuffer, CancellationToken.None);
            await _networkStreamWrapper.WriteAsync(buffer, CancellationToken.None);
        }

        public void Write(in ReadOnlySpan<byte> buffer)
        {
            var lengthBuffer = BitConverter.GetBytes(buffer.Length);

            _networkStreamWrapper.Write(lengthBuffer);
            _networkStreamWrapper.Write(buffer);
        }
    }
}