using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.ClientsContracts.Queries;

namespace EventSourcing.Web.Storage
{
    public static class InMemoryDatabase
    {
        public static readonly Dictionary<Guid, Client> Details = new Dictionary<Guid, Client>();
    }
}