using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Queries;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using EventSourcing.Web.Storage;
using StackExchange.Redis;
using InMemoryDatabase = EventSourcing.Web.Storage.InMemoryDatabase;

namespace EventSourcing.Web.Clients.Views.Clients
{
    public class ClientsQueryHandler : ClientsDbContext,
                    IQueryHandler<GetClients, List<ClientListItem>>,
                    IQueryHandler<GetClient, ClientItem>
    {
        public ClientsQueryHandler(IConnectionMultiplexer redis) : base(redis, "Clients") { }

        public Task<List<ClientListItem>> Handle(GetClients query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetAll<ClientListItem>();

            return System.Threading.Tasks.Task.FromResult(client.ToList());
        }

        public Task<ClientItem> Handle(GetClient query, CancellationToken cancellationToken = default(CancellationToken))
        {

            return Task.FromResult(InMemoryDatabase.Details.FirstOrDefault(x => x.Key == query.ClientId).Value);
        }


    }
}
