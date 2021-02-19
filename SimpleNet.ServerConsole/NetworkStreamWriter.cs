using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public class NetworkStreamWriter
    {
        private readonly NetworkStream _networkStream;

        public NetworkStreamWriter(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public async Task WriteAsync(ReadOnlyMemory<byte> buffer)
        {
            var lengthBuffer = BitConverter.GetBytes(buffer.Length);

            await _networkStream.WriteAsync(lengthBuffer);
            await _networkStream.WriteAsync(buffer);
        }
    }
}