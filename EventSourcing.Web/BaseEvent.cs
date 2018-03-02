using System;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web
{
    public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Version { get; set; }
        public EventType EventType { get; set; }
    }
}