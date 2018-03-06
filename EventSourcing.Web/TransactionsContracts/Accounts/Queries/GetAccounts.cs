using System;
using System.Collections.Generic;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Transactions.Domain.Accounts;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Queries
{
    public class GetAccounts : IQuery<IEnumerable<Account>>
    {
        public Guid ClientId { get; private set; }
        public GetAccounts(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
