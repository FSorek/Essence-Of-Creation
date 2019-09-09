using System;
using UnityEngine;


public class RunningState : BaseState
{
    private Vector3 target = Vector3.zero;
    public RunningState(Unit unit) : base(unit)
    {
        
    }

    public override Type Tick()
    {
        target = WaveManager.Instance.Reachpoints[unit.TargetReachpoint].transform.position;
        unit.transform.position =
            Vector3.MoveTowards(unit.transform.position, target, unit.MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(unit.transform.position, target) <= 1f)
        {
            return typeof(GetNextWaypoint);
        }

        return typeof(RunningState);
    }
}
