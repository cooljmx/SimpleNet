using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class NetworkStreamReader
    {
        private const int LengthBufferSize = 4;
        private readonly INetworkStreamWrapper _networkStreamWrapper;

        public NetworkStreamReader(INetworkStreamWrapper networkStreamWrapper)
        {
            _networkStreamWrapper = networkStreamWrapper;
        }

        public async Task<byte[]> ReadAsync()
        {
            var lengthBuffer = new byte[LengthBufferSize];
            var receivedLength = await _networkStreamWrapper.ReadAsync(lengthBuffer, CancellationToken.None);

            if (receivedLength != lengthBuffer.Length)
                throw new InvalidOperationException("Wrong length buffer size");

            var length = BitConverter.ToUInt32(lengthBuffer);
            var buffer = new byte[length];

            receivedLength = await _networkStreamWrapper.ReadAsync(buffer, CancellationToken.None);

            if (receivedLength != length)
                throw new InvalidOperationException("Wrong buffer size");

            return buffer;
        }

        public ReadOnlySpan<byte> Read()
        {
            var lengthBuffer = new byte[LengthBufferSize];
            var receivedLength = _networkStreamWrapper.Read(lengthBuffer);

            if (receivedLength != lengthBuffer.Length)
                throw new InvalidOperationException("Wrong length buffer size");

            var length = BitConverter.ToUInt32(lengthBuffer);
            var buffer = new byte[length];

            receivedLength = _networkStreamWrapper.Read(buffer);

            if (receivedLength != length)
                throw new InvalidOperationException("Wrong buffer size");

            return buffer;
        }
    }
}