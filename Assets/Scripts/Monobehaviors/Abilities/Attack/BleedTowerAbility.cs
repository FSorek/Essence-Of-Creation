﻿using UnityEngine;

/// <summary>
/// A simple DoT attackAbility, low damage, stacks intensity and only works on non-shielded units
/// </summary>

public class BleedTowerAbility : MonoBehaviour, ITowerLastingAbility
{
    public Damage damage;
    [SerializeField] private float duration = 2;
    [SerializeField] private float interval = 1;
    [SerializeField] private bool stacksInDuration = false;


    public void Apply(IUnit unit, int attackerID)
    {
        
    }

    public void Remove(IUnit unit, int attackerID)
    {
        
    }

    public void Tick(IUnit unit, int attackerID)
    {
        unit.TakeDamage(attackerID, damage);
    }

    public float Duration => duration;
    public float Interval => interval;
    public bool Stacks => stacksInDuration;
}