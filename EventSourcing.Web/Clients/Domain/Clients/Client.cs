using System;
using System.Collections.Generic;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;

namespace EventSourcing.Web.Clients.Domain.Clients
{
    public class Client : AggregateRoot
    {
        public Guid ClientId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public List<Guid> Accounts { get; protected set; }

        public Client()
        {

        }

        public Client(Guid aggregateId, string name, string email)
        {
            AggregateId = aggregateId;
            ApplyChange(new ClientCreatedEvent(aggregateId, new ClientInfo(name, email)));
        }

        public void Update(ClientInfo clientInfo)
        {
            ApplyChange(new ClientUpdatedEvent(AggregateId, ClientId, clientInfo));
        }

        public void AddAccount(Guid id)
        {
            Accounts.Add(id);
        }

        private void Apply(ClientCreatedEvent @event)
        {
            ClientId = @event.ClientId;
            AggregateId = @event.Id;
            Name = @event.Data.Name;
            Email = @event.Data.Email;
            Accounts = new List<Guid>();
        }

        private void Apply(NewAccountCreatedEvent @event)
        {
            Accounts.Add(@event.AccountId);
        }

        private void Apply(ClientUpdatedEvent @event)
        {
            Name = @event.Data.Name;
            Email = @event.Data.Email;
        }
    }
}
