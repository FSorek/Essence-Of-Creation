using System;
using System.Collections.Generic;
using Data.Data_Types.Enums;
using Sirenix.Serialization;
using UnityEngine;

// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable 660,661
// Stat should never be used as a key in dictionary or in hashtable

namespace Data.Data_Types
{
    [Serializable]
    public class Stat
    {
        [SerializeField]private float baseValue;
        
        private readonly List<StatModifier> statModifiers = new List<StatModifier>();
        
        private float value;
        private bool isDirty = true;
        private float lastBaseValue = float.MinValue;

        /// <summary>
        ///     The original value of the stat - without modifiers.
        /// </summary>
        public float BaseValue
        {
            get => baseValue;
            set => baseValue = value;
        }

        /// <summary>
        ///     The final value of the stat - with modifiers.
        /// </summary>
        public float ModValue
        {
            get
            {
                if (isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }

                return value;
            }
        }

        public Stat(float baseValue) : this()
        {
            this.baseValue = baseValue;
        }

        public Stat()
        {
        }
        
        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
        }

        public void RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Remove(mod);
        }

        public static Stat operator +(Stat stat, StatModifier modifier)
        {
            stat.isDirty = true;
            stat.statModifiers.Add(modifier);
            return stat;
        }

        public static Stat operator -(Stat stat, StatModifier modifier)
        {
            stat.isDirty = true;
            stat.statModifiers.Remove(modifier);
            return stat;
        }


        public static bool operator ==(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) == value;
        }

        public static bool operator !=(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) != value;
        }

        public static bool operator >(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) > value;
        }

        public static bool operator <(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) < value;
        }

        public static bool operator <=(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) <= value;
        }

        public static bool operator >=(Stat stat, float value)
        {
            return (stat?.ModValue ?? default) >= value;
        }

        public static implicit operator int(Stat stat)
        {
            return stat != null ? Mathf.RoundToInt(stat.ModValue) : default;
        }

        public static implicit operator float(Stat stat)
        {
            return stat?.ModValue ?? default;
        }

        public static implicit operator Stat(float f)
        {
            return new Stat(f);
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float percentageModification = 1f;
            if (statModifiers == null) return finalValue;
            for (int i = 0; i < statModifiers.Count; i++)
                if (statModifiers[i].Type == StatModifierType.Flat)
                    finalValue += statModifiers[i].Value;
                else if (statModifiers[i].Type == StatModifierType.Percent)
                    percentageModification += statModifiers[i].Value;
            return Mathf.Max(0, finalValue * percentageModification);
        }
    }
}