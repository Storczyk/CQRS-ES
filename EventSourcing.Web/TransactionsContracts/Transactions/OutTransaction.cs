using System;

namespace EventSourcing.Web.TransactionsContracts.Transactions
{
    public class OutTransaction: ITransaction
    {
        public decimal Ammount { get; }

        public DateTime Timestamp { get; }

        public OutTransaction(decimal ammount, DateTime timestamp)
        {
            Ammount = ammount;
            Timestamp = timestamp;
        }
    }
}
