﻿using System;
using System.Threading;
using SimpleNet.Infrastructure.Common;

namespace SimpleNet.Infrastructure.Server
{
    public class ServerListener : IDisposable
    {
        private readonly CancellationToken _cancellationToken;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ISocketWrapper _socketWrapper;

        public ServerListener()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _socketWrapper = new SocketWrapper();
            _socketWrapper.Bind(port: 3000);
            _socketWrapper.Listen(2);
        }

        private void AcceptLoop()
        {
            do
            {
                var socketClientHandler = new ClientStreamHandler(_socketWrapper, _cancellationToken);
                socketClientHandler.StartHandle();
            } while (!_cancellationToken.IsCancellationRequested);
        }

        public void Start()
        {
            new Thread(AcceptLoop).Start();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _socketWrapper.Dispose();
        }
    }
}