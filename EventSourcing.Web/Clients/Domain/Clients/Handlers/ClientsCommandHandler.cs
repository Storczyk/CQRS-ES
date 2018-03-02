using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Commands;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;

namespace EventSourcing.Web.Clients.Domain.Clients.Handlers
{
    public class ClientsCommandHandler :
                    ICommandHandler<CreateClient>,
                    ICommandHandler<UpdateClient>
    {
        private readonly ISession _session;
        private readonly IEventBus _eventBus;

        public ClientsCommandHandler(ISession session, IEventBus eventBus)
        {
            _session = session;
            _eventBus = eventBus;
        }

        public async Task Handle(CreateClient command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var aggregateId = Guid.NewGuid();
            var client = new Client(aggregateId, command.Data.Name, command.Data.Email);
            await _session.Add(client, cancellationToken);
            var eventList = await _session.Commit(cancellationToken);
            foreach (var @event in eventList)
            {
                await _eventBus.Publish(@event);
            }
        }

        public async Task Handle(UpdateClient command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = await _session.Get<Client>(command.Id, cancellationToken: cancellationToken);
            client.Update(command.Data);
            await _session.Add(client, cancellationToken);
            var eventList = await _session.Commit(cancellationToken);

            foreach (var @event in eventList)
            {
                await _eventBus.Publish(@event);
            }
        }
    }
}