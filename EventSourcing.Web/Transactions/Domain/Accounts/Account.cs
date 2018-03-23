using System;

namespace EventSourcing.Web.Transactions.Domain.Accounts
{
    public class Account
    {
        public Guid ClientId { get; set; }
        public decimal Balance { get; set; }
        public string Number { get; set; }

        public Account(Guid clientId, string number)
        {
            ClientId = clientId;
            Balance = 0;
            Number = number;
        }
    }
}
