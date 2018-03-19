namespace EventSourcing.Web
{
    public interface ISnapshotable
    {
        Snapshot TakeSnapshot();
        void ApplySnapshot(Snapshot snapshot);
    }
}
