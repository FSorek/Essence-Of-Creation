using System;
using UnityEngine;

public abstract class BaseState
{
    protected Unit unit;
    protected Transform transform;
    public BaseState(Unit unit)
    {
        this.unit = unit;
        this.transform = unit.transform;
    }
    public abstract Type Tick();
}
