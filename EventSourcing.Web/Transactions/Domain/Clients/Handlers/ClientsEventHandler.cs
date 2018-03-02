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
        public ClientDetailView(IConnectionMultiplexer connection) : base(connection) { }

        public Task Handle(ClientCreatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var client = new ClientItem(@event.ClientId, @event.Data.Name, @event.Data.Email);
            Save(@event.Id, @event);

            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var item = GetDetailsItem(@event.Id);
            //item.Name = @event.Data.Name;
            //item.Email = @event.Data.Email;
            //InMemoryDatabase.Details.TryAdd(@event.Id, item);

            Save(@event.Id, @event);
            return Task.CompletedTask;
        }        
    }
}
