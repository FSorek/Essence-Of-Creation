using System;
using UnityEngine;

public class Effect
{
    private readonly float interval;
    private readonly Action<ITakeDamage> tick;

    private bool stacksInDuration;
    private float duration;
    private float lastRecordedTick;
    private float firstAppliedTime;
    private int attackerID;
    public int AttackerId => attackerID;
    public bool StackInDuration => stacksInDuration;

    public Effect(int attackerId, Ability ability)
    {
        this.attackerID = attackerId;
        this.duration = ability.Duration;
        this.interval = ability.Interval;
        this.stacksInDuration = ability.stacksInDuration;
        this.tick = (target) => ability.Apply(target, attackerId);
        lastRecordedTick = 0f;
        firstAppliedTime = GameTime.time;
    }

    public Effect(int attackerId, float duration, float interval, bool stacksInDuration, Action<ITakeDamage> tick)
    {
        this.attackerID = attackerId;
        this.duration = duration;
        this.interval = interval;
        this.tick = tick;
        this.stacksInDuration = stacksInDuration;
        lastRecordedTick = 0f;
        firstAppliedTime = GameTime.time;
    }

    public void Tick(ITakeDamage unit)
    {
        if (GameTime.time - firstAppliedTime >= duration)
            unit.RemoveEffect(this);
        if (GameTime.time - lastRecordedTick >= interval)
        {
            tick(unit);
            lastRecordedTick = GameTime.time;
        }
    }

    internal void Extend()
    {
        duration += duration;
    }
}
