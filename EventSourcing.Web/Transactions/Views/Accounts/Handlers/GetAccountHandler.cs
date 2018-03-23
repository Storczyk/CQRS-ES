using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Storage;
using EventSourcing.Web.Transactions.Domain.Accounts;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;
using StackExchange.Redis;

namespace EventSourcing.Web.Transactions.Views.Accounts.Handlers
{
    public class GetAccountHandler : ClientsDbContext, IQueryHandler<GetAccount, Account>
    {
        private readonly ISession _session;
        private readonly IEventBus _eventBus;

        public GetAccountHandler(IEventBus eventBus, ISession session, IConnectionMultiplexer redis) : base(redis)
        {
            _eventBus = eventBus;
            _session = session;
        }

        public async Task<Account> Handle(GetAccount request, CancellationToken cancellationToken)
        {
            var events = GetEvents(request.AccountId);
            var client = new Client();
            client.LoadFromHistory(events);

            return client.Account;
        }
    }
}
