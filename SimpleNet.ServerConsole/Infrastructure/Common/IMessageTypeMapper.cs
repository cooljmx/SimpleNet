using System;

namespace SimpleNet.ServerConsole.Infrastructure.Common
{
    public interface IMessageTypeMapper
    {
        Type GetType(int messageType);
        int GetMessageType(object value);
    }
}