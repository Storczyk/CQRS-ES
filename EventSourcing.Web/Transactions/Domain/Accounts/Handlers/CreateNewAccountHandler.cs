using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Transactions.Views.Clients;
using EventSourcing.Web.TransactionsContracts.Accounts.Commands;

namespace EventSourcing.Web.Transactions.Domain.Accounts.Handlers
{
    public class CreateNewAccountHandler : ICommandHandler<CreateNewAccount>
    {
        private readonly IAccountNumberGenerator _accountNumberGenerator;


        public CreateNewAccountHandler(IAccountNumberGenerator accountNumberGenerator)
        {
        }

        public Task Handle(CreateNewAccount message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var account = new Account(message.ClientId, _accountNumberGenerator);

            //_store.Append(account.Id, account.PendingEvents.ToArray());
            //return _session.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
