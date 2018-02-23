using System;
using System.Collections.Generic;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Queries
{
    public class GetAccounts : IQuery<IEnumerable<AccountSummary>>
    {
        public Guid ClientId { get; private set; }
        public GetAccounts(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
