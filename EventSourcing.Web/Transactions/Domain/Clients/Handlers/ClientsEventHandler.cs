using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;
using MediatR;
using StackExchange.Redis;

namespace EventSourcing.Web.Transactions.Domain.Clients.Handlers
{
    public class EventHandler : ClientsDbContext,
        INotificationHandler<ClientCreatedEvent>,
        INotificationHandler<ClientUpdatedEvent>,
        INotificationHandler<NewAccountCreatedEvent>, 
        INotificationHandler<NewInTransactionRecorded>,
        INotificationHandler<NewOutTransactionRecorded>
    {
        public EventHandler(IConnectionMultiplexer connection) : base(connection) { }

        public Task Handle(ClientCreatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            Save(@event.Id, @event);
            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            Save(@event.Id, @event);
            return Task.CompletedTask;
        }

        public Task Handle(NewAccountCreatedEvent @event, CancellationToken cancellationToken)
        {
            Save(@event.Id, @event);
            return Task.CompletedTask;
        }

        public Task Handle(NewOutTransactionRecorded @event, CancellationToken cancellationToken)
        {
            Save(@event.Id, @event);
            return Task.CompletedTask;
        }

        public Task Handle(NewInTransactionRecorded @event, CancellationToken cancellationToken)
        {
            Save(@event.Id, @event);
            return Task.CompletedTask;
        }
    }
}
