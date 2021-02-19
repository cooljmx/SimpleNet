using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.ServerConsole
{
    public interface INetworkStreamWrapper : IDisposable
    {
        bool DataAvailable { get; }
        ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken);
        ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken);
    }
}