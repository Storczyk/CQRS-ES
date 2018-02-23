using MediatR;

namespace EventSourcing.Web.Domain.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
