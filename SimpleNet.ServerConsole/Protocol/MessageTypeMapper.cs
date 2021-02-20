using System;
using System.Collections.Generic;
using SimpleNet.Infrastructure.Common;

namespace SimpleNet.ServerConsole.Protocol
{
    internal class MessageTypeMapper : IMessageTypeMapper
    {
        private readonly Dictionary<MessageType, Type> _messageTypeToType = new Dictionary<MessageType, Type>
        {
            {MessageType.HeartBeat, typeof(HeartBeatMessage)}
        };

        private readonly Dictionary<Type, MessageType> _typeToMessageType = new Dictionary<Type, MessageType>();

        public MessageTypeMapper()
        {
            foreach (var (key, value) in _messageTypeToType)
                _typeToMessageType.Add(value, key);
        }

        public Type GetType(int messageType)
        {
            return _messageTypeToType[(MessageType) messageType];
        }

        public int GetMessageType(object value)
        {
            return (int) _typeToMessageType[value.GetType()];
        }
    }
}