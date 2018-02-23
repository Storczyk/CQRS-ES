using System;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Transactions.Domain.Accounts
{
    public class Account : EventSource
    {
        public Guid ClientId { get; private set; }
        public decimal Balance { get; private set; }
        public string Number { get; private set; }

        public Account() { }

        public Account(Guid clientId, IAccountNumberGenerator numberGenerator)
        {
            /*var @event = new NewAccountCreatedEvent
            {
                AccountId = Guid.NewGuid(),
                ClientId = clientId,
                Number = numberGenerator.Generate(),
            };
            */
            //Apply(@event);
            //Append(@event);
        }

        public void RecordInTransaction(Guid fromId, decimal amount)
        {
            var @event = new NewInTransactionRecorded(fromId, Id, new InTransaction(amount, DateTime.Now));
            Apply(@event);
            Append(@event);
        }

        public void RecordOutTransaction(Guid toId, decimal amount)
        {
            var @event = new NewOutTransactionRecorded(Id, toId,new OutTransaction(amount,DateTime.Now));
            Apply(@event);
            Append(@event);
        }

        public void Apply(NewAccountCreatedEvent @event)
        {
            Id = @event.AccountId;
            ClientId = @event.ClientId;
            Number = @event.Number;
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
