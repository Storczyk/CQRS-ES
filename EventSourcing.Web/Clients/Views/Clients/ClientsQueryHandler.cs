using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Queries;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using EventSourcing.Web.Storage;
using StackExchange.Redis;
using InMemoryDatabase = EventSourcing.Web.Storage.InMemoryDatabase;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.Clients.Views.Clients
{
    public class ClientsQueryHandler : ClientsDbContext,
                    IQueryHandler<GetClients, List<Client>>,
                    IQueryHandler<GetClient, Client>
    {
        public ClientsQueryHandler(IConnectionMultiplexer redis) : base(redis) { }

        public Task<List<Client>> Handle(GetClients query, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var client = GetAll<Client>();
            var events = Load<ClientCreatedEvent>().Where(d => d.EventType==EventType.ClientCreated);
            var list = new List<Client>();
            foreach (var clientCreatedEvent in events)
            {
                Client client = new Client();
                var listT = new List<IEvent>() { clientCreatedEvent };
                client.LoadFromHistory(listT);

                var clientEvents = GetEvents(client.AggregateId);
                if (clientEvents.Any())
                {
                    client.LoadFromHistory(clientEvents);
                }

                list.Add(client);
            }
            return System.Threading.Tasks.Task.FromResult(list);
        }

        public Task<Client> Handle(GetClient query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var events = GetEvents(query.ClientId);
            var client = new Client();
            var list = new List<IEvent> {  };
            client.LoadFromHistory(events);
            return Task.FromResult(client);
        }
    }
}
