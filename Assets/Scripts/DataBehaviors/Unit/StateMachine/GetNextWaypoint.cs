using System;
using UnityEngine;
public class GetNextWaypoint : BaseState
{
    public GetNextWaypoint(Unit unit) : base(unit)
    {

    }

    public override Type Tick()
    {
        if (unit.TargetReachpoint >= 11)
        {
            unit.TargetReachpoint = -1;
        }
        unit.TargetReachpoint++;

        return typeof(RunningState);
    }
}
