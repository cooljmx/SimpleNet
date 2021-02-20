using System;

namespace SimpleNet.Infrastructure.Server
{
    internal class RequestPipeline : IRequestPipeline
    {
        private readonly IRequestHandlerFactory _requestHandlerFactory;

        public RequestPipeline(IRequestHandlerFactory requestHandlerFactory)
        {
            _requestHandlerFactory = requestHandlerFactory;
        }

        public void Push(Guid id, object request)
        {
            _requestHandlerFactory.Create(id, request);
        }
    }
}