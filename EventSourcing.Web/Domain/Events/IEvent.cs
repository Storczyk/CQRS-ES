using System;
using MediatR;

namespace EventSourcing.Web.Domain.Events
{
    public interface IEvent : INotification
    {
        Guid Id { get; set; }
        DateTime TimeStamp { get; set; }
        int Version { get; set; }
    }
}
