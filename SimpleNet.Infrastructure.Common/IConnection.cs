namespace SimpleNet.Infrastructure.Common
{
    public interface IConnection
    {
        void Subscribe<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent;

        void Unsubscribe<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent;
    }
}