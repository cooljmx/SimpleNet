using System;
using SimpleNet.Infrastructure.Common;
using SimpleNet.Infrastructure.Server;
using SimpleNet.Protocol;

namespace SimpleNet.Server
{
    internal class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IMessageTypeMapper _messageTypeMapper;
        private readonly IResponsePipeline _responsePipeline;

        public RequestHandlerFactory(
            IMessageTypeMapper messageTypeMapper,
            IResponsePipeline responsePipeline)
        {
            _messageTypeMapper = messageTypeMapper;
            _responsePipeline = responsePipeline;
        }

        public void Create(Guid id, object request)
        {
            var messageType = (MessageType) _messageTypeMapper.GetMessageType(request);

            switch (messageType)
            {
                case MessageType.GetFileRequest:
                    new GetFileRequestHandler(id, (GetFileRequest) request, _responsePipeline).Handle();
                    return;
                case MessageType.HeartBeatRequest:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}