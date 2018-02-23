using System;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Queries
{
    public class GetAccount : IQuery<AccountSummary>
    {
        public Guid AccountId { get; private set; }

        public GetAccount(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
