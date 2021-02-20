using System;

namespace SimpleNet.Infrastructure.Common
{
    public class DeserializedMessage
    {
        public int Type { get; set; }
        public Guid Id { get; set; }
        public object Value { get; set; }
    }
}