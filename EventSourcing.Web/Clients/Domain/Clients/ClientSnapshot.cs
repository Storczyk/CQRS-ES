using EventSourcing.Web.Transactions.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Web.Clients.Domain.Clients
{
    public class ClientSnapshot : Snapshot
    {
        public Guid ClientId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public Account Account { get; protected set; }

        public ClientSnapshot(Guid id, Guid clientId, Guid aggregateId, int version, string name, string email, Account account) :
            base(clientId, aggregateId, version)
        {
            ClientId = clientId;
            Id = id;
            Name = name;
            Email = email;
            Account = account;
        }
    }
}
