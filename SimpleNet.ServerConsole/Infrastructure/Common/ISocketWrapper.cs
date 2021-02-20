using System;
using System.Net.Sockets;

namespace SimpleNet.ServerConsole.Infrastructure.Common
{
    public interface ISocketWrapper : IDisposable
    {
        bool Connected { get; }
        void Bind(string address = null, int port = 0);
        void Listen(int count);
        INetworkStreamWrapper CreateNetworkStream();
        ISocketWrapper Accept();
        void Connect(string address, int port);
        void Shutdown(SocketShutdown socketShutdown);
        void Disconnect();
    }
}