using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Web
{
    public interface ISnapshotStorageProvider
    {
        int SnapshotFrequency { get; }
        Task<Snapshot> GetSnapshotAsync(Type aggregateType, Guid aggregateId);
        Task SaveSnapshotAsync(Type aggregateType, Snapshot snapshot);
    }
}
