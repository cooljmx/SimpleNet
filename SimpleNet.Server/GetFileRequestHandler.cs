using System;
using SimpleNet.Infrastructure.Server;
using SimpleNet.Protocol;

namespace SimpleNet.Server
{
    internal class GetFileRequestHandler
    {
        private readonly Guid _id;
        private readonly object _request;
        private readonly IResponsePipeline _responsePipeline;

        public GetFileRequestHandler(
            Guid id, 
            GetFileRequest request,
            IResponsePipeline responsePipeline)
        {
            _id = id;
            _request = request;
            _responsePipeline = responsePipeline;
        }

        public void Handle()
        {
            var getFileResponse = new GetFileResponse();

            _responsePipeline.Push(_id, getFileResponse);
        }
    }
}