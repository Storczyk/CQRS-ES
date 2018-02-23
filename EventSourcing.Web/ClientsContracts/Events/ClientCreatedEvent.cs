using System;
using EventSourcing.Web.ClientsContracts.ValueObjects;

namespace EventSourcing.Web.ClientsContracts.Events
{
    public class ClientCreatedEvent : BaseEvent
    {
        public Guid ClientId { get; }
        public ClientInfo Data { get; }

        public ClientCreatedEvent(Guid id, ClientInfo data, Guid clientId)
        {
            Id = id;
            Data = data;
            ClientId = clientId;
        }
    }
}
