using System;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Transactions.Views.Accounts.AccountSummary
{
    /*public class AccountSummaryViewProjection : ViewProjection<AccountSummaryView, Guid>
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public int TransactionsCount { get; set; }
        public bool IsDeleted { get; set; }

        public void ApplyEvent(NewAccountCreatedEvent @event)
        {
            AccountId = @event.AccountId;
            Balance = 0;
            ClientId = @event.ClientId;
            Number = @event.Number;
            TransactionsCount = 0;
        }

        public void ApplyEvent(NewInTransactionRecorded @event)
        {
            Balance += @event.Inflow.Ammount;
        }

        public void ApplyEvent(NewOutTransactionRecorded @event)
        {
            Balance -= @event.Outflow.Ammount;
        }

        internal void ApplyEvent(ClientCreatedEvent @event)
        {
            ClientName = @event.Data.Name;
        }

        internal void ApplyEvent(ClientUpdatedEvent @event)
        {
            ClientName = @event.Data.Name;
        }

        internal void ApplyEvent(ClientDeletedEvent @event)
        {
            IsDeleted = false;
        }
    }*/
}
