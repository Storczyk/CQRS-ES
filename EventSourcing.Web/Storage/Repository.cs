using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.Domain.Events;
using StackExchange.Redis;

namespace EventSourcing.Web.Storage
{
    public class Repository : ClientsDbContext, IRepository
    {
        private readonly IEventStore _eventStore;
        private readonly ISnapshotStorageProvider snapshotStorageProvider;

        public int SnapshotFrequency { get; }

        public Repository(IConnectionMultiplexer redis, IEventStore eventStore, ISnapshotStorageProvider snapshotStorageProvider) : base(redis)
        {
            _eventStore = eventStore;
            this.snapshotStorageProvider = snapshotStorageProvider;
            SnapshotFrequency = 10;
        }

        public async Task<List<IEvent>> Save<T>(T aggregate, int? expectedVersion = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
        {
            if (expectedVersion != null &&
                (await _eventStore.Get(aggregate.AggregateId, expectedVersion.Value, cancellationToken)).Any())
            {
                //different versio found
            }

            var changes = aggregate.FlushUncommitedChanges();

            return await _eventStore.Save(changes, cancellationToken);
        }

        public async Task<T> Get<T>(Guid aggregateId, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
        {
            var events = await _eventStore.Get(aggregateId, -1, cancellationToken);
            if (!events.Any())
            {
                events = base.GetEvents(aggregateId);
                //not found
            }

            var aggregate = (T)Activator.CreateInstance(typeof(T));
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public T GetSnapshot<T>(Guid aggregateId) where T:ISnapshotable
        {
            var p = default(T);

            var l = base.Load<Snapshot>(aggregateId.ToString());
            foreach (var item in l)
            {
                var isSnapshotable = typeof(ISnapshotable).IsAssignableFrom(item.GetType());
                if (isSnapshotable)
                {
                    ((ISnapshotable)p).ApplySnapshot(item);
                    return p;
                }
            }

        }

        public void SaveSnapshot(Snapshot snapshot)
        {
            base.Save<Snapshot>(snapshot);
        }
    }
}
