using System;
using EventSourcing.Web.ClientsContracts.ValueObjects;

namespace EventSourcing.Web.ClientsContracts.Events
{
    public class ClientCreatedEvent : BaseEvent
    {
        public Guid ClientId { get; set; }
        public ClientInfo Data { get; set; }

        public ClientCreatedEvent() { }
        public ClientCreatedEvent(Guid agregateId, ClientInfo data)
        {
            ClientId = Guid.NewGuid();
            Id = agregateId;
            Data = data;
            EventType = EventType.ClientCreated;
        }
    }
}