using System;

namespace EventSourcing.Web.TransactionsContracts.Transactions.Events
{
    public class NewOutTransactionRecorded : BaseEvent
    {
        public Guid FromAccountId { get; }
        public Guid ToAccountId { get; }
        public OutTransaction Outflow { get; }

        public NewOutTransactionRecorded(Guid fromAccountId, Guid toAccountId, OutTransaction outflow)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Outflow = outflow;
        }
    }
}
