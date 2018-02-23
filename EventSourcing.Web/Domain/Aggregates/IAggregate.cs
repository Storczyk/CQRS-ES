using System;

namespace EventSourcing.Web.Domain.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
