using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    private readonly List<StatModifier> statModifiers;
    private bool isDirty = true;
    private float _value;
    private float lastBaseValue = float.MinValue;

    public float BaseValue;


    public Stat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public Stat()
    {
        statModifiers = new List<StatModifier>();
    }

    public static bool operator +(Stat stat, StatModifier modifier)
    {
        stat.isDirty = true;
        stat.statModifiers.Add(modifier);
        return true;
    }

    public static bool operator -(Stat stat, StatModifier modifier)
    {
        stat.isDirty = true;
        return stat.statModifiers.Remove(modifier);
    }

    public static bool operator ==(Stat stat, float value) => stat.Value == value;
    public static bool operator !=(Stat stat, float value) => stat.Value != value;
    public static bool operator >(Stat stat, float value) => stat.Value > value;
    public static bool operator <(Stat stat, float value) => stat.Value < value;
    public static bool operator <=(Stat stat, float value) => stat.Value <= value;
    public static bool operator >=(Stat stat, float value) => stat.Value >= value;
    public static implicit operator int(Stat stat) => Mathf.RoundToInt(stat.Value);
    public static implicit operator float(Stat stat) => stat.Value;

    public float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float percentageModification = 1f;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModifierType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModifierType.Percent)
            {
                percentageModification += mod.Value;
            }
        }

        finalValue *= percentageModification;

        return finalValue;
    }
}
