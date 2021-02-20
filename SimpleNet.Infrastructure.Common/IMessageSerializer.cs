using System;

namespace SimpleNet.Infrastructure.Common
{
    public interface IMessageSerializer
    {
        ReadOnlySpan<byte> Serialize<T>(T message, Guid id);
        DeserializedMessage Deserialize(ReadOnlySpan<byte> buffer);
    }
}