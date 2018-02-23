﻿using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Transactions.Views.Accounts.AccountSummary;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;

namespace EventSourcing.Web.Transactions.Views.Accounts.Handlers
{
    public class GetAccountHandler : IQueryHandler<GetAccount, TransactionsContracts.Accounts.ValueObjects.AccountSummary>
    {

        public GetAccountHandler()
        {
        }

        public Task<TransactionsContracts.Accounts.ValueObjects.AccountSummary> Handle(GetAccount request, CancellationToken cancellationToken)
        {
            /*return _session.Query<AccountSummaryView>()
                .Select(a => new TransactionsContracts.Accounts.ValueObjects.AccountSummary
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    ClientId = a.ClientId,
                    Number = a.Number,
                    TransactionsCount = a.TransactionsCount
                }).FirstOrDefaultAsync(p => p.AccountId == request.AccountId, cancellationToken);*/
            return null;
        }
    }
}
