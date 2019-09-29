using System;
using UnityEngine;

public class Effect
{
    private int ownerID;
    private bool stacksInDuration;
    private float interval;
    private Action<IUnit> tick;
    private Action<IUnit> apply;
    private Action<IUnit> remove;
    private float duration;
    private float lastRecordedTick;
    private float firstAppliedTime;
    private bool applied = false;

    public Effect(int ownerId, IAbility ability)
    {
        this.ownerID = ownerId;
        this.duration = ability.Duration;
        this.interval = ability.Interval;
        this.stacksInDuration = ability.StacksInDuration;
        this.tick = (target) => ability.Tick(target, ownerId);
        this.apply = (target) => ability.Apply(target, ownerId);
        this.remove = (target) => ability.Remove(target, ownerId);
        lastRecordedTick = GameTime.time;
        firstAppliedTime = GameTime.time;
    }

    public Effect(int ownerId, float duration, float interval, bool stacksInDuration, Action<IUnit> tick, Action<IUnit> apply, Action<IUnit> remove)
    {
        this.ownerID = ownerId;
        this.duration = duration;
        this.interval = interval;
        this.tick = tick;
        this.stacksInDuration = stacksInDuration;
        this.apply = apply;
        this.remove = remove;
        lastRecordedTick = GameTime.time;
        firstAppliedTime = GameTime.time;
    }

    public void Tick(IUnit unit)
    {
        if (!applied)
        {
            apply(unit);
            applied = true;
        }
        if (GameTime.time - firstAppliedTime >= duration)
        {
            remove(unit);
            unit.RemoveEffect(this);
        }
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
    public Action<IUnit> EffectTick => tick;
}
