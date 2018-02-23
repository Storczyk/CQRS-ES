using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;
using MediatR;

namespace EventSourcing.Web.Transactions.Domain.Clients.Handlers
{
    public class ClientDetailView :
        INotificationHandler<ClientCreatedEvent>
    {
        public Task Handle(ClientCreatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            InMemoryDatabase.Details.TryAdd(@event.Id, new ClientItem(@event.ClientId, @event.Data.Name, @event.Data.Email));
            return Task.CompletedTask;
        }
        /*
        public Task Handle(ClientUpdatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            var item = GetDetailsItem(@event.Id);
            item.Name = @event.Data.Name;
            item.Email = @event.Data.Email;
            return Task.CompletedTask;
        }

        public Task Handle(ClientDeletedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            InMemoryDatabase.Details.Remove(@event.Id);
            return Task.CompletedTask;
        }*/
        
        private static ClientItem GetDetailsItem(Guid id)
        {
            if (!InMemoryDatabase.Details.TryGetValue(id, out var item))
            {
                //not found
            }

            return item;
        }
    }
}
