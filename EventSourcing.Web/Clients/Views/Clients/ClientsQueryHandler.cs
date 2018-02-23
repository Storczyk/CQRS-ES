using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Queries;


namespace EventSourcing.Web.Clients.Views.Clients
{
    public class ClientsQueryHandler :
                    IQueryHandler<GetClients, List<ClientListItem>>,
                    IQueryHandler<GetClient, ClientItem>
    {
        private IQueryable<Client> Clients;

        public ClientsQueryHandler()
        {
            Clients = ClientsDbContext.Clients.AsQueryable();
        }

        public Task<List<ClientListItem>> Handle(GetClients query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var clients = Clients.Select(client => new ClientListItem
            {
                Id = client.Id,
                Name = client.Name
            }).ToList();

            return System.Threading.Tasks.Task.FromResult(clients);
        }

        public Task<ClientItem> Handle(GetClient query, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            return Task.FromResult(new ClientItem(Guid.NewGuid(),"", ""));
        }
    }
}
