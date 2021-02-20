using System;

namespace SimpleNet.Infrastructure.Server
{
    public interface IRequestHandlerFactory
    {
        void Create(Guid id, object request);
    }
}