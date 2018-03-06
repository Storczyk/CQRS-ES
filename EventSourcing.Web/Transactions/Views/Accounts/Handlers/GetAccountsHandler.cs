using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public class GetAccountsHandler : ClientsDbContext, IQueryHandler<GetAccounts, IEnumerable<Account>>
    {
        private readonly ISession _session;
        private readonly IEventBus _eventBus;

        public GetAccountsHandler(IEventBus eventBus, ISession session, IConnectionMultiplexer redis) : base(redis)
        {
            _eventBus = eventBus;
            _session = session;
        }

        public async Task<IEnumerable<Account>> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            var events = Load<NewAccountCreatedEvent>(request.ClientId.ToString());
            var accounts = new List<Account>();
            foreach(var @event in events)
            {
                var account = new Account();
                var listT = new List<IEvent>() { @event };
                account.LoadFromHistory(listT);

                accounts.Add(account);
            }

            return accounts;
        }
    }
}
