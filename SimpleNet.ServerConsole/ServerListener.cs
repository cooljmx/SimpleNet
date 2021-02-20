using System;
using System.Threading;

namespace SimpleNet.ServerConsole
{
    internal class ServerListener : IDisposable
    {
        private readonly ISocketWrapper _socketWrapper;
        private readonly CancellationToken _cancellationToken;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public ServerListener()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _socketWrapper = new SocketWrapper();
            _socketWrapper.Bind(port: 3000);
            _socketWrapper.Listen(2);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _socketWrapper.Dispose();
        }

        private void AcceptLoop()
        {
            do
            {
                var socketClientHandler = new ClientHandler(_socketWrapper, _cancellationToken);
                socketClientHandler.StartHandle();
            } while (!_cancellationToken.IsCancellationRequested);
        }

        public void Start()
        {
            new Thread(AcceptLoop).Start();
        }
    }
}