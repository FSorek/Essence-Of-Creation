using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        var randomOffset = Random.insideUnitCircle + new Vector2(2f, 2f);
        targetPosition = waveManager.Reachpoints[targetWaypoint++].position + new Vector3(randomOffset.x, 0, randomOffset.y);
    }


    public override Type Tick()
    {
        movement.Move(targetPosition, speed);
        if (Vector3.Distance(unit.Position, targetPosition) <= .5f)
        {
            var randomOffset = Random.insideUnitCircle + new Vector2(2f,2f);
            targetPosition = waveManager.Reachpoints[targetWaypoint++].position + new Vector3(randomOffset.x, 0, randomOffset.y);
        }

        return typeof(UnitRunState);
    }
}
