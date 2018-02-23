using System;
using EventSourcing.Web.Domain.Commands;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Commands
{
    public class CreateNewAccount : ICommand
    {
        public Guid ClientId { get; set; }
    }
}
