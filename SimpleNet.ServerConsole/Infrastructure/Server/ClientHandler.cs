using System;
using System.Diagnostics;
using System.Threading;
using SimpleNet.ServerConsole.Infrastructure.Common;

namespace SimpleNet.ServerConsole.Infrastructure.Server
{
    internal class ClientHandler
    {
        private readonly CancellationToken _cancellationToken;
        private readonly ISocketWrapper _serverSocketWrapper;
        private NetworkStreamReader _networkStreamReader;
        private INetworkStreamWrapper _networkStreamWrapper;
        private NetworkStreamWriter _networkStreamWriter;
        private ISocketWrapper _socketWrapper;

        public ClientHandler(ISocketWrapper serverSocketWrapper, CancellationToken cancellationToken)
        {
            _serverSocketWrapper = serverSocketWrapper;
            _cancellationToken = cancellationToken;
        }

        private void HandleClient()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_networkStreamWrapper.DataAvailable)
                {
                    stopwatch.Restart();

                    var buffer = _networkStreamReader.Read();

                    _networkStreamWriter.Write(buffer);
                }

                Thread.Sleep(10);
            } while (!_cancellationToken.IsCancellationRequested && stopwatch.ElapsedMilliseconds < 5000);

            _networkStreamWrapper.Dispose();
            _socketWrapper.Dispose();
        }

        public void StartHandle()
        {
            if (_socketWrapper != null)
                throw new InvalidOperationException("Already handled");

            _socketWrapper = _serverSocketWrapper.Accept();
            _networkStreamWrapper = _socketWrapper.CreateNetworkStream();
            _networkStreamReader = new NetworkStreamReader(_networkStreamWrapper);
            _networkStreamWriter = new NetworkStreamWriter(_networkStreamWrapper);

            new Thread(HandleClient).Start();
        }
    }
}