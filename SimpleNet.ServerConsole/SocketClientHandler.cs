using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleNet.ServerConsole
{
    internal class SocketClientHandler
    {
        private readonly ISocketWrapper _serverSocketWrapper;
        private bool _isStopped;
        private NetworkStreamReader _networkStreamReader;
        private INetworkStreamWrapper _networkStreamWrapper;
        private NetworkStreamWriter _networkStreamWriter;
        private ISocketWrapper _socketWrapper;
        private Thread _thread;


        public SocketClientHandler(ISocketWrapper serverSocketWrapper)
        {
            _serverSocketWrapper = serverSocketWrapper;
        }

        public void StopHandle()
        {
            _isStopped = true;
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
            } while (!_isStopped && stopwatch.ElapsedMilliseconds < 5000);

            _thread = null;
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

            _thread = new Thread(HandleClient);
            _thread.Start();
        }
    }
}