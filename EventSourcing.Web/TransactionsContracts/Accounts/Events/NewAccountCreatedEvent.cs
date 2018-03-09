using System;

namespace EventSourcing.Web.TransactionsContracts.Accounts.Events
{
    public class NewAccountCreatedEvent : BaseEvent
    {
        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string Number { get; set; }

        public NewAccountCreatedEvent() { }

        public NewAccountCreatedEvent(Guid aggregateId, string accountNumber, Guid clientId)
        {
            Id = aggregateId;
            Number = accountNumber;
            ClientId = clientId;
            AccountId = Guid.NewGuid();
            EventType = EventType.AccountCreated;
        }
    }
}
