public interface IStatus
{
    int SourceID { get; }
    bool Stacks { get; }

    void Extend();
    bool Tick(IUnit owner);
    bool Equals(object obj);
}
