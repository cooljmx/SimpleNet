using System;

namespace SimpleNet.Infrastructure.Server
{
    public interface IResponsePipelineListener
    {
        void Invoke(Guid id, int type, object response);
    }
}