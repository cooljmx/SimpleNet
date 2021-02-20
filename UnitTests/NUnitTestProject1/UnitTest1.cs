using NUnit.Framework;
using SimpleNet.ServerConsole.Infrastructure.Common;
using SimpleNet.ServerConsole.Protocol;

namespace NUnitTestProject1
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var messageTypeMapper = new MessageTypeMapper();
            var messageSerializer = new MessageSerializer(messageTypeMapper);

            var heartBeatMessage = new HeartBeatMessage();

            var buffer = messageSerializer.Serialize(heartBeatMessage);
            var deserializedMessage = messageSerializer.Deserialize(buffer);
        }
    }
}