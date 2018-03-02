using System;
using System.Collections.Generic;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Domain.Queries;

namespace EventSourcing.Web.ClientsContracts.Queries
{
    public class GetClients : IQuery<List<Client>>
    {
    }
}
