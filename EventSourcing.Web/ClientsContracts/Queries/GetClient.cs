using System;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Domain.Queries;

namespace EventSourcing.Web.ClientsContracts.Queries
{
    public class GetClient : IQuery<Client>
    {
        public Guid ClientId { get; }

        public GetClient(Guid id)
        {
            ClientId = id;
        }

    }
}