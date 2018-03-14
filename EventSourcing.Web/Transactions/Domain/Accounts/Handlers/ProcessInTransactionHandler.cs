using System.Threading;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Domain.ValueObjects;
using EventSourcing.Web.Storage;
using EventSourcing.Web.TransactionsContracts.Transactions.Commands;
using MediatR;
using StackExchange.Redis;

namespace EventSourcing.Web.Transactions.Domain.Accounts.Handlers
{
    public class ProcessInTransactionHandler : ClientsDbContext, ICommandHandler<MakeTransfer>
    {
        private readonly ISession _session;
        private readonly IEventBus _eventBus;

        public ProcessInTransactionHandler(IEventBus eventBus, ISession session, IConnectionMultiplexer redis) : base(redis)
        {
            _eventBus = eventBus;
            _session = session;
        }

        async System.Threading.Tasks.Task IRequestHandler<MakeTransfer>.Handle(MakeTransfer command, CancellationToken cancellationToken)
        {
            var accountFromEvents = GetEvents(command.FromAccountId);
            var accountToEvents = GetEvents(command.ToAccountId);
            var accountFrom = new Account();
            var accountTo = new Account();
            accountFrom.LoadFromHistory(accountFromEvents);
            accountTo.LoadFromHistory(accountToEvents);
            accountFrom.RecordOutTransaction(command.ToAccountId, command.Amount);
            accountTo.RecordInTransaction(command.FromAccountId, command.Amount);
            await _session.Add(accountTo, cancellationToken);
            await _session.Add(accountFrom, cancellationToken);
            var events = await _session.Commit();
            foreach(var @event in events)
            {
                await _eventBus.Publish(@event);
            }
            /* var accountFrom = await _store.AggregateStreamAsync<Account>(command.FromAccountId, token: cancellationToken);

             accountFrom.RecordOutTransaction(command.ToAccountId, command.Amount);
             _store.Append(accountFrom.Id, accountFrom.PendingEvents.ToArray());

             var accountTo = await _store.AggregateStreamAsync<Account>(command.ToAccountId, token: cancellationToken);

             accountTo.RecordInTransaction(command.FromAccountId, command.Amount);
             _store.Append(accountTo.Id, accountTo.PendingEvents.ToArray());

             await _session.SaveChangesAsync(cancellationToken);*/
        }
    }
}
