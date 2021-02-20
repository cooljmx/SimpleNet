using ProtoBuf;

namespace SimpleNet.Protocol
{
    [ProtoContract]
    public class GetFileRequest
    {
        [ProtoMember(1)] public string FileName { get; set; }
    }
}