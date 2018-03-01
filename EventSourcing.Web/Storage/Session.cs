using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Web.Domain.Events;

namespace EventSourcing.Web.Storage
{
    public class Session : ISession
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Guid, AggregateDescriptor> _trackedAggregates;

        public Session(IRepository repository)
        {
            _repository = repository;
            _trackedAggregates = new Dictionary<Guid, AggregateDescriptor>();
        }

        public Task Add<T>(T aggregate, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.AggregateId))
            {
                _trackedAggregates.Add(aggregate.AggregateId, new AggregateDescriptor { Aggregate = aggregate, Version = aggregate.Version });
            }
            else if (_trackedAggregates[aggregate.AggregateId].Aggregate != aggregate)
            {
                //different aggregate
            }
            return Task.FromResult(0);
        }

        public async Task<T> Get<T>(Guid id, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
        {
            if (IsTracked(id))
            {
                var trackedAggregate = (T)_trackedAggregates[id].Aggregate;
                if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
                {
                    //different aggregate
                }

                return trackedAggregate;
            }

            var aggregate = await _repository.Get<T>(id, cancellationToken);
            if (expectedVersion != null && aggregate.Version != expectedVersion)
            {
                //different aggregate
            }

            await Add(aggregate, cancellationToken);
            return aggregate;
        }

        public async Task<List<IEvent>> Commit(CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = new List<IEvent>();

            foreach (var value in _trackedAggregates.Values)
            {
                var x =await _repository.Save(value.Aggregate, value.Version, cancellationToken);
                list.AddRange(x);
            }
            _trackedAggregates.Clear();
            return list;
        }

        private bool IsTracked(Guid aggregateId)
        {
            return _trackedAggregates.ContainsKey(aggregateId);
        }

        private class AggregateDescriptor
        {
            public AggregateRoot Aggregate { get; set; }
            public int Version { get; set; }
        }
    }
}
