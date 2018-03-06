using System;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Transactions.Domain.Accounts;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Queries
{
    public class GetAccount : IQuery<Account>
    {
        public Guid AccountId { get; private set; }

        public GetAccount(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
