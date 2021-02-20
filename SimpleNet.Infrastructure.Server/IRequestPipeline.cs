using System;

namespace SimpleNet.Infrastructure.Server
{
    public interface IRequestPipeline
    {
        void Push(Guid id, object request);
    }
}