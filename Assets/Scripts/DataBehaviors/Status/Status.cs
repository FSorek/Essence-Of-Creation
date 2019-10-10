
public abstract class Status : IStatus
{
    protected readonly int sourceID;
    protected readonly bool stacks;
    private bool applied = false;

    public Status(int sourceID, bool stacks)
    {
        this.sourceID = sourceID;
        this.stacks = stacks;
    }

    public virtual bool Tick(IUnit owner) // when returns true means the status expired
    {
        if (!applied)
        {
            Apply(owner);
            applied = true;
        }

        if (Expired())
        {
            Remove(owner);
            return true;
        }

        Process(owner);
        return false;
    }

    protected abstract void Remove(IUnit unit);
    protected abstract void Apply(IUnit owner);
    protected abstract void Process(IUnit owner);
    protected abstract bool Expired();
    public abstract override bool Equals(object obj);

    public abstract void Extend();

    public override int GetHashCode()
    {
        var hashCode = -945275016;
        hashCode = hashCode * -1521134295 + sourceID.GetHashCode();
        hashCode = hashCode * -1521134295 + Stacks.GetHashCode();
        return hashCode;
    }

    public int SourceID => sourceID;

    public virtual bool Stacks => stacks; // stacks in numbers
}
