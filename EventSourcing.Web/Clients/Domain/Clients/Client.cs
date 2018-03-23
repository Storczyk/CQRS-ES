using System;
using System.Collections.Generic;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Transactions.Domain.Accounts;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Clients.Domain.Clients
{
    public class Client : AggregateRoot, ISnapshotable
    {
        public Guid ClientId { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public Account Account { get; protected set; }

        public Client() { }

        public Client(Guid aggregateId, string name, string email)
        {
            AggregateId = aggregateId;
            ApplyChange(new ClientCreatedEvent(aggregateId, new ClientInfo(name, email)));
        }

        public void Update(ClientInfo clientInfo)
        {
            ApplyChange(new ClientUpdatedEvent(AggregateId, ClientId, clientInfo));
        }
        
        public void AddAccount(IAccountNumberGenerator accountNumberGenerator)
        {
            ApplyChange(new NewAccountCreatedEvent(AggregateId, accountNumberGenerator.Generate(), ClientId));
        }

        public void RecordInTransaction(Guid fromId, decimal amount)
        {
            ApplyChange(new NewInTransactionRecorded(fromId, AggregateId, new InTransaction(amount, DateTime.Now)));
        }

        public void RecordOutTransaction(Guid toId, decimal amount)
        {
            ApplyChange(new NewOutTransactionRecorded(AggregateId, toId, new OutTransaction(amount, DateTime.Now)));
        }

        private void Apply(ClientCreatedEvent @event)
        {
            ClientId = @event.ClientId;
            AggregateId = @event.Id;
            Name = @event.Data.Name;
            Email = @event.Data.Email;
            Account = null;
        }

        public void Apply(NewAccountCreatedEvent @event)
        {
            Account = new Account(@event.ClientId, @event.Number);
        }

        public void Apply(ClientUpdatedEvent @event)
        {
            Name = @event.Data.Name;
            Email = @event.Data.Email;
        }

        public void Apply(NewInTransactionRecorded @event)
        {
            Account.Balance += @event.Inflow.Ammount;
        }

        public void Apply(NewOutTransactionRecorded @event)
        {
            Account.Balance -= @event.Outflow.Ammount;
        }

        public Snapshot TakeSnapshot()
        {
            return new ClientSnapshot(Guid.NewGuid(), ClientId, AggregateId, Version, Name, Email, Account);
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
