public interface ISusceptible
{
    void AddStatus(IStatus status);
    IStatus[] CurrentStatuses { get; }
}
