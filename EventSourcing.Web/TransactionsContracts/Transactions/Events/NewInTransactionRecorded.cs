using System;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.TransactionsContracts.Transactions.Events
{
    public class NewInTransactionRecorded : BaseEvent
    {
        public Guid FromAccountId { get; }
        public Guid ToAccountId { get; }
        public InTransaction Inflow { get; }

        public NewInTransactionRecorded(Guid fromAccountId, Guid toAccountId, InTransaction inflow)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Inflow = inflow;
            EventType = EventType.TransferIncome;
        }
    }
}
