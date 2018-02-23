using System.Threading.Tasks;

namespace EventSourcing.Web.Domain.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
