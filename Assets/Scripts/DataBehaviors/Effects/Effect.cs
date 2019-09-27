using System;
using UnityEngine;

public class Effect
{
    private int ownerID;
    private bool stacksInDuration;
    private float interval;
    private Action<ITakeDamage> tick;
    private float duration;
    private float lastRecordedTick;
    private float firstAppliedTime;


    public Effect(int ownerId, Ability ability)
    {
        this.ownerID = ownerId;
        this.duration = ability.Duration;
        this.interval = ability.Interval;
        this.stacksInDuration = ability.StacksInDuration;
        this.tick = (target) => ability.Apply(target, ownerId);
        lastRecordedTick = GameTime.time;
        firstAppliedTime = GameTime.time;
    }

    public Effect(int ownerId, float duration, float interval, bool stacksInDuration, Action<ITakeDamage> tick)
    {
        this.ownerID = ownerId;
        this.duration = duration;
        this.interval = interval;
        this.tick = tick;
        this.stacksInDuration = stacksInDuration;
        lastRecordedTick = GameTime.time;
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

    public void Extend()
    {
        duration += duration;
    }

    public int OwnerId => ownerID;
    public bool StackInDuration => stacksInDuration;
    public float Duration => duration;
    public Action<ITakeDamage> EffectTick => tick;
}
