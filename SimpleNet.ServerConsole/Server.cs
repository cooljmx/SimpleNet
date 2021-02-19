using System;
using System.Threading;

namespace SimpleNet.ServerConsole
{
    public class Server : IDisposable
    {
        private readonly ISocketWrapper _socketWrapper;
        private bool _isStopped;

        public Server()
        {
            _socketWrapper = new SocketWrapper();
            _socketWrapper.Bind(port: 3000);
            _socketWrapper.Listen(2);
        }

        public void Dispose()
        {
            _isStopped = true;

            _socketWrapper.Dispose();
        }

        private void AcceptLoop()
        {
            do
            {
                var socketClientHandler = new SocketClientHandler(_socketWrapper);
                socketClientHandler.StartHandle();
            } while (!_isStopped);
        }

        public void Start()
        {
            _isStopped = false;

            var acceptThread = new Thread(AcceptLoop);
            acceptThread.Start();
        }
    }
}