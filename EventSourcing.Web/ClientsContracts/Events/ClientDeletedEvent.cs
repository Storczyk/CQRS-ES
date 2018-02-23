using System;

namespace EventSourcing.Web.ClientsContracts.Events
{
    public class ClientDeletedEvent : BaseEvent
    {
        public Guid ClientId { get; }

        public ClientDeletedEvent(Guid id, Guid clientId)
        {
            Id = id;
            ClientId = clientId;
        }
    }
}
