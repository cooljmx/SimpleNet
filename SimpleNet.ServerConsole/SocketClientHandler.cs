using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

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

        private async Task HandleClientAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_networkStreamWrapper.DataAvailable)
                {
                    stopwatch.Restart();

                    var buffer = await _networkStreamReader.ReadAsync();

                    await _networkStreamWriter.WriteAsync(buffer);
                }

                await Task.Delay(10);
            } while (!_isStopped && stopwatch.ElapsedMilliseconds < 5000);

            _networkStreamWrapper.Dispose();
            _socketWrapper.Dispose();
        }

        public async Task StartHandleAsync()
        {
            if (_socketWrapper != null)
                throw new InvalidOperationException("Already handled");

            _socketWrapper = await _serverSocketWrapper.AcceptAsync();
            _networkStreamWrapper = _socketWrapper.CreateNetworkStream();
            _networkStreamReader = new NetworkStreamReader(_networkStreamWrapper);
            _networkStreamWriter = new NetworkStreamWriter(_networkStreamWrapper);

            _thread = new Thread(async () => await HandleClientAsync());
            _thread.Start();
        }
    }
}