using System.Net.Sockets;

namespace SimpleNet.ServerConsole
{
    public class SocketWrapper : ISocketWrapper
    {
        private readonly Socket _socket;

        public SocketWrapper(Socket socket)
        {
            _socket = socket;
        }
    }
}