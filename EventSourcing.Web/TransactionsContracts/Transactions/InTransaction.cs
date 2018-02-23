using System;

namespace EventSourcing.Web.TransactionsContracts.Transactions
{
    public class InTransaction : ITransaction
    {
        public decimal Ammount { get; }

        public DateTime Timestamp { get; }

        public InTransaction(decimal ammount, DateTime timestamp)
        {
            Ammount = ammount;
            Timestamp = timestamp;
        }
    }
}
