﻿using UnityEngine;

/// <summary>
/// A simple DoT ability, high damage, stacks duration
/// </summary>
public class BurningAbility : MonoBehaviour, IAbility
{
    public Damage Damage;
    [SerializeField] private float duration = 2;
    [SerializeField] private float interval = 1;
    [SerializeField] private bool stacksInDuration = true;

    public void Apply(IUnit unit, int attackerID)
    {
        
    }

    public void Remove(IUnit unit, int attackerID)
    {
        
    }

    public void Tick(IUnit unit, int attackerID)
    {
        unit.TakeDamage(attackerID, Damage);
    }

    public float Duration => duration;
    public float Interval => interval;
    public bool StacksInDuration => stacksInDuration;
}
