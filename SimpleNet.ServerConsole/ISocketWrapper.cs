using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public interface ISocketWrapper : IDisposable
    {
        void Bind(string address = null, int port = 0);
        void Listen(int count);
        INetworkStreamWrapper CreateNetworkStream();
        Task<ISocketWrapper> AcceptAsync();
        void Connect(string address, int port);
        bool Connected { get; }
        void Shutdown(SocketShutdown socketShutdown);
        void Disconnect();
    }
}