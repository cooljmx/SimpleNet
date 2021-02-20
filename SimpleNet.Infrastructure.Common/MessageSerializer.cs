using System;
using System.IO;
using ProtoBuf;

namespace SimpleNet.Infrastructure.Common
{
    internal class MessageSerializer : IMessageSerializer
    {
        private readonly IMessageTypeMapper _messageTypeMapper;

        public MessageSerializer(IMessageTypeMapper messageTypeMapper)
        {
            _messageTypeMapper = messageTypeMapper;
        }

        public ReadOnlySpan<byte> Serialize<T>(T message, Guid id)
        {
            var messageType = _messageTypeMapper.GetMessageType(message);

            using var memoryStream = new MemoryStream();

            var messageTypeBuffer = BitConverter.GetBytes(messageType);
            memoryStream.Write(messageTypeBuffer);
            memoryStream.Write(id.ToByteArray());
            Serializer.Serialize(memoryStream, message);

            return memoryStream.ToArray();
        }

        public DeserializedMessage Deserialize(ReadOnlySpan<byte> buffer)
        {
            using var memoryStream = new MemoryStream(buffer.ToArray());
            var messageTypeBuffer = new byte[4];
            memoryStream.Read(messageTypeBuffer);

            var messageType = BitConverter.ToInt32(messageTypeBuffer);
            var type = _messageTypeMapper.GetType(messageType);

            var idBuffer = new byte[16];
            memoryStream.Read(idBuffer);

            var deserializedMessage = new DeserializedMessage
            {
                Type = messageType,
                Id = new Guid(idBuffer),
                Value = Serializer.Deserialize(type, memoryStream)
            };

            return deserializedMessage;
        }
    }
}