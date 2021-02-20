using System;
using System.IO;
using ProtoBuf;

namespace SimpleNet.ServerConsole.Infrastructure.Common
{
    internal class MessageSerializer
    {
        private readonly IMessageTypeMapper _messageTypeMapper;

        public MessageSerializer(IMessageTypeMapper messageTypeMapper)
        {
            _messageTypeMapper = messageTypeMapper;
        }

        public ReadOnlySpan<byte> Serialize<T>(T message)
        {
            var messageType = _messageTypeMapper.GetMessageType(message);

            using var memoryStream = new MemoryStream();

            var messageTypeBuffer = BitConverter.GetBytes(messageType);
            memoryStream.Write(messageTypeBuffer);
            Serializer.Serialize(memoryStream, message);

            return memoryStream.ToArray();
        }

        public DeserializedMessage Deserialize(ReadOnlySpan<byte> buffer)
        {
            using var memoryStream = new MemoryStream(buffer.ToArray());
            var messageTypeBuffer = new byte[4];
            memoryStream.Read(messageTypeBuffer, 0, 4);

            var messageType = BitConverter.ToInt32(messageTypeBuffer);
            var type = _messageTypeMapper.GetType(messageType);

            var deserializedMessage = new DeserializedMessage
            {
                Type = messageType,
                Value = Serializer.Deserialize(type, memoryStream)
            };

            return deserializedMessage;
        }
    }
}