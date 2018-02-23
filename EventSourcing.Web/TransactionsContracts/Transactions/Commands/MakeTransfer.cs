using System;
using EventSourcing.Web.Domain.Commands;

namespace EventSourcing.Web.TransactionsContracts.Transactions.Commands
{
    public class MakeTransfer : ICommand
    {
        public Guid FromAccountId { get; }
        public Guid ToAccountId { get; }
        public decimal Amount { get; }

        public MakeTransfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
        }
    }
}
