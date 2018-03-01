using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcing.Web.ClientsContracts.Queries;

namespace EventSourcing.Web.Storage
{
    public static class InMemoryDatabase
    {
        public static readonly Dictionary<Guid, ClientItem> Details = new Dictionary<Guid, ClientItem>();
    }
}