using System;
using UnityEngine;

/// <summary>
/// A simple DoT attackAbility, high damage, stacks duration
/// </summary>
public class BurningTowerAbility : MonoBehaviour, ITowerLastingAbility
{
    public Damage Damage;
    [SerializeField] private float duration = 2;
    [SerializeField] private float interval = 1;
    [SerializeField] private bool stacks = false;
    private Burning burning;

    public void Apply(IUnit unit, int attackerID)
    {
        burning = new Burning(attackerID, duration, interval, stacks);
    }

    public void Remove(IUnit unit, int attackerID)
    {
        
    }

    public void Tick(IUnit unit, int attackerID)
    {
        
    }

    public float Duration => duration;
    public float Interval => interval;
    public bool Stacks => stacks;
}
