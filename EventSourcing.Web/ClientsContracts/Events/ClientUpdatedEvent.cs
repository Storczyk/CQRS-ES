using System;
using EventSourcing.Web.ClientsContracts.ValueObjects;

namespace EventSourcing.Web.ClientsContracts.Events
{
    public class ClientUpdatedEvent : BaseEvent
    {
        public ClientInfo Data { get; }

        public ClientUpdatedEvent(Guid id, ClientInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
