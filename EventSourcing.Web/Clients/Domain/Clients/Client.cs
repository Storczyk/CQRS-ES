using System;
using System.Collections.Generic;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Transactions.Domain.Accounts;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;

namespace EventSourcing.Web.Clients.Domain.Clients
{
    public class Client : AggregateRoot, ISnapshotable
    {
        public Guid ClientId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public Account Account { get; protected set; }

        public Client()
        {
            Account = new Account();
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
            Account = new Account();
        }

        public void AddAccount(IAccountNumberGenerator accountNumberGenerator)
        {
            ApplyChange(new NewAccountCreatedEvent(AggregateId, accountNumberGenerator.Generate(), ClientId));
        }

        private void Apply(NewAccountCreatedEvent @event)
        {
            var account = new Account();
            account.Apply(@event);
            Account = account;
        }

        private void Apply(ClientUpdatedEvent @event)
        {
            Name = @event.Data.Name;
            Email = @event.Data.Email;
        }

        public Snapshot TakeSnapshot()
        {
            return new ClientSnapshot(Guid.NewGuid(),ClientId, AggregateId, Version,Name, Email, Account);
        }

        public void ApplySnapshot(Snapshot snapshot)
        {
            ClientSnapshot item = (ClientSnapshot)snapshot;
            AggregateId = item.AggregateId;
            ClientId = item.ClientId;
            Version = item.Version;
            Name = item.Name;
            Email = item.Email;

        }
    }
}
