public abstract class UnitAbility
{
    protected Unit unit;
    public UnitAbility(Unit unit)
    {
        this.unit = unit;
    }
    public abstract void OnApplied();
    public abstract void OnFinished();
}
