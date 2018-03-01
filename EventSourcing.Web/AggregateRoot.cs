using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcing.Web.Domain.Events;
using ReflectionMagic;

namespace EventSourcing.Web
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes = new List<IEvent>();

        public Guid AggregateId { get; protected set; }
        public int Version { get; protected set; }

        public IEvent[] GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        public IEvent[] FlushUncommitedChanges()
        {
            lock (_changes)
            {
                var changes = _changes.ToArray();
                var i = 0;
                foreach (var @event in changes)
                {
                    if (@event.Id == Guid.Empty && AggregateId == Guid.Empty)
                    {
                        throw new AggregateException($"{GetType()}, {@event.GetType()}");
                    }
                    if (@event.Id == Guid.Empty)
                    {
                        @event.Id = AggregateId;
                    }
                    i++;
                    @event.Version = Version + i;
                    @event.TimeStamp = DateTime.Now;
                }
                Version = Version + changes.Length;
                _changes.Clear();
                return changes;
            }
        }

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            lock (_changes)
            {
                foreach (var e in history.ToArray())
                {
                    if (e.Version != Version + 1)
                    {
                        throw new AggregateException($"{e.Id}");
                    }
                    ApplyEvent(e);
                    AggregateId = e.Id;
                    Version++;
                }
            }
        }

        public void ApplyChange(IEvent @event)
        {
            lock (_changes)
            {
                ApplyEvent(@event);
                _changes.Add(@event);
            }
        }

        protected virtual void ApplyEvent(IEvent @event)
        {
            try
            {
                this.AsDynamic().Apply(@event);
            }
            catch { }
        }
    }
}
