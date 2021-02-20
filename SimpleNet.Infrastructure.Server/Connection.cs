using System;
using System.Collections.Generic;
using SimpleNet.Infrastructure.Common;

namespace SimpleNet.Infrastructure.Server
{
    internal class Connection : IConnection, IResponsePipelineListener, IDisposable
    {
        private readonly IRequestPipeline _requestPipeline;
        private readonly IResponsePipeline _responsePipeline;

        private readonly Dictionary<Type, ISet<IEventSubscriber>> _subscriptions =
            new Dictionary<Type, ISet<IEventSubscriber>>();

        public Connection(
            IRequestPipeline requestPipeline,
            IResponsePipeline responsePipeline)
        {
            _requestPipeline = requestPipeline;
            _responsePipeline = responsePipeline;

            _responsePipeline.Subscribe(this);
        }

        public void Subscribe<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent
        {
            if (!_subscriptions.TryGetValue(typeof(TEvent), out var eventSubscribers))
            {
                eventSubscribers = new HashSet<IEventSubscriber>();
                _subscriptions.TryAdd(typeof(TEvent), eventSubscribers);
            }

            eventSubscribers.Add(subscriber);
        }

        public void Unsubscribe<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent
        {
            if (_subscriptions.TryGetValue(typeof(TEvent), out var eventSubscribers))
                eventSubscribers.Remove(subscriber);
        }

        public void Invoke(Guid id, int type, object response)
        {
        }

        public void Dispose()
        {
            _responsePipeline.Unsubscribe(this);
        }
    }
}