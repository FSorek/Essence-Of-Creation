using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    private readonly List<StatModifier> statModifiers;
    private bool isDirty = true;
    private float _value;
    private float lastBaseValue = float.MinValue;

    /// <summary>
    /// The original value of the stat - without modifiers.
    /// </summary>
    public float BaseValue { get; set; }

    /// <summary>
    /// The final value of the stat - with modifiers.
    /// </summary>
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

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float percentageModification = 1f;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            if (statModifiers[i].Type == StatModifierType.Flat)
            {
                finalValue += statModifiers[i].Value;
            }
            else if (statModifiers[i].Type == StatModifierType.Percent)
            {
                percentageModification += statModifiers[i].Value;
            }
        }
        return finalValue * percentageModification;
    }
}
