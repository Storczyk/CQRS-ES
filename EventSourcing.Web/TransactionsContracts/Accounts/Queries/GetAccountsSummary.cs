using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Queries
{
    public class GetAccountsSummary : IQuery<AllAccountsSummary>
    {
    }
}
