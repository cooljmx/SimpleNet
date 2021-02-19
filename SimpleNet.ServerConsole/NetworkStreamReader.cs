using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class NetworkStreamReader
    {
        public const int LengthBufferSize = 4;
        private readonly NetworkStream _networkStream;

        public NetworkStreamReader(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public async Task<byte[]> ReadAsync()
        {
            var lengthBuffer = new byte[LengthBufferSize];
            var receivedLength = await _networkStream.ReadAsync(lengthBuffer);

            if (receivedLength != lengthBuffer.Length)
                throw new InvalidOperationException("Wrong length buffer size");

            var length = BitConverter.ToUInt32(lengthBuffer);
            var buffer = new byte[length];

            receivedLength = await _networkStream.ReadAsync(buffer);

            if (receivedLength != length)
                throw new InvalidOperationException("Wrong buffer size");

            return buffer;
        }
    }
}