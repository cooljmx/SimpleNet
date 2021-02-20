using ProtoBuf;

namespace SimpleNet.Protocol
{
    [ProtoContract]
    public class GetFileResponse
    {
        [ProtoMember(1)] public byte[] Data { get; set; }
    }
}