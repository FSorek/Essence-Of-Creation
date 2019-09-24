using System;

public abstract class UnitState
{
    protected IEntity unit;

    public UnitState(IEntity unit)
    {
        this.unit = unit;
    }

    public abstract Type Tick();
}
