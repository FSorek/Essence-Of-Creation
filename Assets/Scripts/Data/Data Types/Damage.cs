
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Damage
{
    [SerializeField] private Stat _earth;
    [SerializeField] private Stat _water;
    [SerializeField] private Stat _air;
    [SerializeField] private Stat _fire;

    public Stat Fire { get => _fire; }
    public Stat Air { get => _air; }
    public Stat Water { get => _water; }
    public Stat Earth { get => _earth; }

    public Damage(int fire, int air, int water, int earth)
    {
        _fire = fire;
        _air = air;
        _water = water;
        _earth = earth;
    }

    public Elements GetDominatingDamage()
    {
        var dict = new Dictionary<Elements, Stat>()
        {
            {Elements.Fire, Fire},
            {Elements.Water, Water},
            {Elements.Earth, Earth},
            {Elements.Air, Air}
        };
        var highestVal = Mathf.Max(Fire, Water, Earth, Air);
        return dict.Where(e => e.Value == highestVal).First().Key;
    }

    public static Damage operator +(Damage d1, Damage d2)
    {
        d1._fire += d2.Fire;
        d1._air += d2.Air;
        d1._water += d2.Water;
        d1._earth += d2.Earth;

        return d1;
    }

    public static Damage operator *(Damage d1, Damage d2)
    {
        d1._fire *= d2.Fire;
        d1._air *= d2.Air;
        d1._water *= d2.Water;
        d1._earth *= d2.Earth;

        return d1;
    }
}
