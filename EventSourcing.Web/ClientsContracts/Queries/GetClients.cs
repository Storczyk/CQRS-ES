using System;
using System.Collections.Generic;
using EventSourcing.Web.Domain.Queries;

namespace EventSourcing.Web.ClientsContracts.Queries
{
    public class ClientListItem
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
    }

    public class GetClients : IQuery<List<ClientListItem>>
    {
    }
}
