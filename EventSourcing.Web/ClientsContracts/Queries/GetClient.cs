using System;
using EventSourcing.Web.Domain.Queries;

namespace EventSourcing.Web.ClientsContracts.Queries
{
    public class ClientItem
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ClientItem(Guid clientId, string name, string email)
        {
            ClientId = clientId;
            Name = name;
            Email = email;
        }
    }

    public class GetClient : IQuery<ClientItem>
    {
        public Guid ClientId { get; }

        public GetClient(Guid id)
        {
            ClientId = id;
        }

    }
}
