using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.Storage
{
    public interface IEventStore
    {
        Task<List<IEvent>> Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken));

    }
}
