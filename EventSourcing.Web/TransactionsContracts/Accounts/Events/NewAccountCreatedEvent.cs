using System;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Events
{
    public class NewAccountCreatedEvent : BaseEvent
    {
        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string Number { get; set; }

        public NewAccountCreatedEvent(Guid id)
        {
            Id = id;
            AccountId = Guid.NewGuid();
        }
    }
}
