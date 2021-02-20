using System;
using System.Collections.Generic;
using SimpleNet.Infrastructure.Common;

namespace SimpleNet.Infrastructure.Server
{
    internal class ResponsePipeline : IResponsePipeline
    {
        private readonly HashSet<IResponsePipelineListener> _listeners = new HashSet<IResponsePipelineListener>();
        private readonly IMessageTypeMapper _messageTypeMapper;

        public ResponsePipeline(IMessageTypeMapper messageTypeMapper)
        {
            _messageTypeMapper = messageTypeMapper;
        }

        public void Push(Guid id, object response)
        {
            var messageType = _messageTypeMapper.GetMessageType(response);

            foreach (var listener in _listeners)
                listener.Invoke(id, messageType, response);
        }

        public void Subscribe(IResponsePipelineListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unsubscribe(IResponsePipelineListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}