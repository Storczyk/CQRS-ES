using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.Storage
{
    public interface IRepository
    {
        Task<List<IEvent>> Save<T>(T aggregate, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
        Task<T> Get<T>(Guid aggregateId, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
    }
}