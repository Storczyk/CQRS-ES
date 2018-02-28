using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;
using MediatR;
using StackExchange.Redis;

namespace EventSourcing.Web.Transactions.Domain.Clients.Handlers
{
    public class ClientDetailView : ClientsDbContext,
        INotificationHandler<ClientCreatedEvent>,
        INotificationHandler<ClientUpdatedEvent>
    {
        public ClientDetailView(IConnectionMultiplexer connection) : base(connection, "Clients") { }

        public Task Handle(ClientCreatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            InMemoryDatabase.List.Add(new ClientListItem
            {
                Id = @event.ClientId,
                Name = @event.Data.Name
            });
            var client = new ClientItem(@event.ClientId, @event.Data.Name, @event.Data.Email);
            InMemoryDatabase.Details.TryAdd(@event.Id, client);
            Save(@event.Id, client);
            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            var item = GetDetailsItem(@event.Id);
            item.Name = @event.Data.Name;
            item.Email = @event.Data.Email;
            InMemoryDatabase.Details.TryAdd(@event.Id, item);
            return Task.CompletedTask;
        }

        private static ClientItem GetDetailsItem(Guid id)
        {
            if (!InMemoryDatabase.Details.TryGetValue(id, out var item))
            {
                //not found
            }

            InMemoryDatabase.Details.Remove(id);
            return item;
        }
    }
}
