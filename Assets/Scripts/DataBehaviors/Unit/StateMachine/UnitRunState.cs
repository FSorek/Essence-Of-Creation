using System;
using UnityEngine;

public class UnitRunState : UnitState
{

    private EntitySimpleMove movement;
    private float speed;
    private int targetWaypoint;
    private IWaveManager waveManager;
    private Vector3 targetPosition;


    public UnitRunState(ICanMove unit, IWaveManager waveManager) : base(unit)
    {
        movement = new EntitySimpleMove(unit);
        speed = unit.MovementSpeed;
        targetWaypoint = 0;
        this.waveManager = waveManager;
        targetPosition = waveManager.Reachpoints[targetWaypoint].position;
    }


    public override Type Tick()
    {
        movement.Move(targetPosition, speed);
        if (Vector3.Distance(unit.Position, targetPosition) <= 1f)
            targetPosition = waveManager.Reachpoints[targetWaypoint++].position;

        return typeof(UnitRunState);
    }
}
