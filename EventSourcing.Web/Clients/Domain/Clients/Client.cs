using System;
using System.Collections.Generic;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Transactions.Domain.Accounts;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;

namespace EventSourcing.Web.Clients.Domain.Clients
{
    public class Client : AggregateRoot
    {
        public Guid ClientId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public List<Account> Accounts { get; protected set; }

        public Client()
        {
            Accounts = new List<Account>();
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

        private void Apply(ClientCreatedEvent @event)
        {
            ClientId = @event.ClientId;
            AggregateId = @event.Id;
            Name = @event.Data.Name;
            Email = @event.Data.Email;
            Accounts = new List<Account>();
        }

        public void AddAccount(IAccountNumberGenerator accountNumberGenerator)
        {
            ApplyChange(new NewAccountCreatedEvent(AggregateId, accountNumberGenerator.Generate(), ClientId));
        }

        private void Apply(NewAccountCreatedEvent @event)
        {
            var account = new Account();
            account.Apply(@event);
            Accounts.Add(account);
        }

        private void Apply(ClientUpdatedEvent @event)
        {
            Name = @event.Data.Name;
            Email = @event.Data.Email;
        }
    }
}
