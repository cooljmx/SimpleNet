using System;

namespace SimpleNet.Infrastructure.Server
{
    public interface IResponsePipeline
    {
        void Push(Guid id, object response);

        void Subscribe(IResponsePipelineListener listener);
        void Unsubscribe(IResponsePipelineListener listener);
    }
}