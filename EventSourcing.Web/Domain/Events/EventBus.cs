using System.Threading.Tasks;
using MediatR;

namespace EventSourcing.Web.Domain.Events
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return _mediator.Publish(@event);
        }
    }
}
