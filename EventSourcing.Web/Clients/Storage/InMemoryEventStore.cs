using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Storage;
using StackExchange.Redis;

namespace EventSourcing.Web.Clients.Storage
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> _db = new Dictionary<Guid, List<IEvent>>();

        public async Task<List<IEvent>> Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            var d = new List<IEvent>();
            foreach (var @event in events)
            {
                _db.TryGetValue(@event.Id, out var list);
                if (list == null)
                {
                    list = new List<IEvent>();
                    _db.Add(@event.Id, list);
                }
                list.Add(@event);
                d.AddRange(list);
            }
            return events.ToList();
        }

        public async Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            _db.TryGetValue(aggregateId, out var events);
            return events ?? new List<IEvent>();
        }
    }
}