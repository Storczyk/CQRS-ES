using System;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using EventSourcing.Web.TransactionsContracts.Transactions.Events;

namespace EventSourcing.Web.Transactions.Views.Accounts.AllAccountsSummary
{
    public class AllAccountsSummaryView
    {
        public Guid Id { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalBalance { get; set; }

        public int TotalTransactionsCount { get; set; }

        public void ApplyEvent(NewAccountCreatedEvent @event)
        {
            TotalCount++;
        }

        public void ApplyEvent(NewInTransactionRecorded @event)
        {
            TotalBalance += @event.Inflow.Ammount;
            TotalTransactionsCount++;
        }

        public void ApplyEvent(NewOutTransactionRecorded @event)
        {
            TotalBalance -= @event.Outflow.Ammount;
            TotalTransactionsCount++;
        }
    }
}
