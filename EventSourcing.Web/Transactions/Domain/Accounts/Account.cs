using System;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Transactions.Domain.Accounts
{
    public class Account : AggregateRoot
    {
        public Guid ClientId { get; private set; }
        public decimal Balance { get; private set; }
        public string Number { get; private set; }

        public Account() { }

        public Account(Guid aggregateId, IAccountNumberGenerator numberGenerator, Guid clientId)
        {
            ApplyChange(new NewAccountCreatedEvent(aggregateId, numberGenerator.Generate(), clientId));
        }

        public void RecordInTransaction(Guid fromId, decimal amount)
        {
            ApplyChange(new NewInTransactionRecorded(fromId, AggregateId, new InTransaction(amount, DateTime.Now)));
        }

        public void RecordOutTransaction(Guid toId, decimal amount)
        {
            ApplyChange(new NewOutTransactionRecorded(AggregateId, toId, new OutTransaction(amount, DateTime.Now)));
        }

        public void Apply(NewAccountCreatedEvent @event)
        {
            ClientId = @event.ClientId;
            AggregateId = @event.Id;
            Number = @event.Number;
            Version = @event.Version;
        }

        public void Apply(NewInTransactionRecorded @event)
        {
            Balance += @event.Inflow.Ammount;
        }

        public void Apply(NewOutTransactionRecorded @event)
        {
            Balance -= @event.Outflow.Ammount;
        }
    }
}
