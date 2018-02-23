using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Transactions.Views.Accounts.AccountSummary;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;

namespace EventSourcing.Web.Transactions.Views.Accounts.Handlers
{
    public class GetAccountsHandler : IQueryHandler<GetAccounts, IEnumerable<TransactionsContracts.Accounts.ValueObjects.AccountSummary>>
    {

        public GetAccountsHandler()
        {
        }

        public async Task<IEnumerable<TransactionsContracts.Accounts.ValueObjects.AccountSummary>> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            /*var result = await _session.Query<AccountSummaryView>()
                .Select(
                    a => new TransactionsContracts.Accounts.ValueObjects.AccountSummary
                    {
                        AccountId = a.AccountId,
                        Balance = a.Balance,
                        ClientId = a.ClientId,
                        Number = a.Number,
                        TransactionsCount = a.TransactionsCount
                    }
                ).Where(p => p.ClientId == request.ClientId)
                .ToListAsync(cancellationToken);
                */
            return null;
        }
    }
}
