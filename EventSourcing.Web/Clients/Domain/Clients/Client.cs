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

        public Client(Guid id, string name, string email)
        {
            Id = id;
            ApplyChange(new ClientCreatedEvent(id, new ClientInfo(name, email), Guid.NewGuid()));
        }

        public void Update(ClientInfo clientInfo)
        {
            ApplyChange(new ClientUpdatedEvent(Id, clientInfo));
        }

        public void AddAccount(Guid id)
        {
            Accounts.Add(id);
        }

        private void Apply(ClientCreatedEvent @event)
        {
            ClientId = @event.ClientId;
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
