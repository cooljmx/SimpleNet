namespace SimpleNet.Infrastructure.Common
{
    public interface IEventSubscriber
    {
        void Handle(object @event);
    }
}