using System.Net;
using System.Net.Sockets;

namespace SimpleNet.ServerConsole
{
    public class SocketWrapper : ISocketWrapper
    {
        private readonly Socket _socket;

        public SocketWrapper()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private SocketWrapper(Socket socket)
        {
            _socket = socket;
        }

        public void Bind(string address = null, int port = 0)
        {
            var ipAddress = address == null
                ? IPAddress.Any
                : IPAddress.Parse(address);

            var ipEndPoint = new IPEndPoint(ipAddress, port);

            _socket.Bind(ipEndPoint);
        }

        public void Listen(int count)
        {
            _socket.Listen(count);
        }

        public INetworkStreamWrapper CreateNetworkStream()
        {
            return new NetworkStreamWrapper(_socket);
        }

        public ISocketWrapper Accept()
        {
            var socket = _socket.Accept();

            var socketWrapper = new SocketWrapper(socket);

            return socketWrapper;
        }

        public void Connect(string address, int port)
        {
            var ipAddress = IPAddress.Parse(address);

            _socket.Connect(ipAddress, port);
        }

        public bool Connected => _socket.Connected;

        public void Shutdown(SocketShutdown socketShutdown)
        {
            _socket.Shutdown(socketShutdown);
        }

        public void Disconnect()
        {
            _socket.Disconnect(false);
        }

        public void Dispose()
        {
            _socket.Close();
            _socket.Dispose();
        }
    }
}