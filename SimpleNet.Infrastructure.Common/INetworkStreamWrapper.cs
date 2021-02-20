using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNet.Infrastructure.Common
{
    public interface INetworkStreamWrapper : IDisposable
    {
        bool DataAvailable { get; }
        ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken);
        ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken);
        int Read(Span<byte> buffer);
        void Write(ReadOnlySpan<byte> buffer);
    }
}