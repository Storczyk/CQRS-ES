using System.Threading;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.ValueObjects;
using EventSourcing.Web.TransactionsContracts.Transactions.Commands;
using MediatR;

namespace EventSourcing.Web.Transactions.Domain.Accounts.Handlers
{
    public class ProcessInTransactionHandler : ICommandHandler<MakeTransfer>
    {
        public ProcessInTransactionHandler()
        {
        }

        async System.Threading.Tasks.Task IRequestHandler<MakeTransfer>.Handle(MakeTransfer command, CancellationToken cancellationToken)
        {
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
