using System;
using UnityEngine;

public class Effect
{
    private readonly float interval;
    private readonly Action<Unit> tick;

    private float duration;
    private float lastRecordedTick;
    private float firstAppliedTime;
    private string attackerName;
    public string AttackerName => attackerName;

    public Effect(string attackerName, float duration, float interval, Action<Unit> tick)
    {
        this.attackerName = attackerName;
        this.duration = duration;
        this.interval = interval;
        this.tick = tick;
        lastRecordedTick = 0f;
        firstAppliedTime = Time.time;
    }

    public void Tick(Unit unit)
    {
        if (Time.time - firstAppliedTime >= duration)
            unit.RemoveEffect(this);
        if (Time.time - lastRecordedTick >= interval)
        {
            tick(unit);
            lastRecordedTick = Time.time;
        }
    }

    internal void Extend()
    {
        duration += duration;
    }
}
