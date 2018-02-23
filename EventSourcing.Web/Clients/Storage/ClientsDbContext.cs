using System.Collections.Generic;
using EventSourcing.Web.Clients.Domain.Clients;

namespace EventSourcing.Web.Clients.Storage
{
    public class ClientsDbContext
    {
        public static List<Client> Clients;

        static ClientsDbContext()
        {
            Clients = new List<Client>();
        }
    }
}
