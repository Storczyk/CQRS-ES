using System;
using EventSourcing.Web.ClientsContracts.ValueObjects;

namespace EventSourcing.Web.ClientsContracts.Events
{
    public class ClientUpdatedEvent : BaseEvent
    {
        public Guid ClientId { get; }
        public ClientInfo Data { get; }

        public ClientUpdatedEvent(Guid id, Guid clientId, ClientInfo data)
        {
            Id = id;
            ClientId = clientId;
            Data = data;
        }
    }
}