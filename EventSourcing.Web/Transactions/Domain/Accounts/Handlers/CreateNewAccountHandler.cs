using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;
using EventSourcing.Web.TransactionsContracts.Accounts.Commands;

namespace EventSourcing.Web.Transactions.Domain.Accounts.Handlers
{
    public class CreateNewAccountHandler : ICommandHandler<CreateNewAccount>
    {
        private readonly IAccountNumberGenerator _accountNumberGenerator;
        private readonly ISession _session;
        private readonly IEventBus _eventBus;

        public CreateNewAccountHandler(IAccountNumberGenerator accountNumberGenerator, ISession session, IEventBus eventBus)
        {
            _accountNumberGenerator = accountNumberGenerator;
            _eventBus = eventBus;
            _session = session;
        }

        public async Task Handle(CreateNewAccount message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = await _session.Get<Client>(message.AggregateId, cancellationToken: cancellationToken);
            client.AddAccount(_accountNumberGenerator);
            //var account = new Account(message.AggregateId, _accountNumberGenerator, client.ClientId);
            await _session.Add(client, cancellationToken);
            var eventList = await _session.Commit(cancellationToken);
            foreach (var @event in eventList)
            {
                await _eventBus.Publish(@event);
            }
        }
    }
}
