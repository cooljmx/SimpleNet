using System;

namespace SimpleNet.Infrastructure.Common
{
    public interface IMessageTypeMapper
    {
        Type GetType(int messageType);
        int GetMessageType(object value);
    }
}