using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.Storage
{
    public interface ISession
    {
        Task Add<T>(T aggregate, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
        Task<T> Get<T>(Guid id, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
        Task<List<IEvent>> Commit(CancellationToken cancellationToken = default(CancellationToken));
    }
}
