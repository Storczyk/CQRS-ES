using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Transactions.Views.Accounts.AllAccountsSummary;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;

namespace EventSourcing.Web.Transactions.Views.Accounts.Handlers
{
    public class GetAccountsSummaryHandler : IQueryHandler<GetAccountsSummary, TransactionsContracts.Accounts.ValueObjects.AllAccountsSummary>
    {
        public GetAccountsSummaryHandler()
        {
        }

        public Task<TransactionsContracts.Accounts.ValueObjects.AllAccountsSummary> Handle(GetAccountsSummary message, CancellationToken cancellationToken = default(CancellationToken))
        {
            /*return _session.Query<AllAccountsSummaryView>()
                .Select(
                    a => new TransactionsContracts.Accounts.ValueObjects.AllAccountsSummary
                    {
                        TotalBalance = a.TotalBalance,
                        TotalCount = a.TotalCount,
                        TotalTransactionsCount = a.TotalTransactionsCount
                    }
                )
                .SingleOrDefaultAsync(cancellationToken);*/
            return null;
        }
    }
}
