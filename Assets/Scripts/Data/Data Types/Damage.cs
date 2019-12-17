using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Data.Data_Types
{
    public abstract class Damage
    {
        [ShowInInspector]protected Stat value;
        public Stat Value => value;

        public abstract int GetDamageToArmor(ArmorType armorType);

        public Damage(int baseValue)
        {
            value = baseValue;
        }
        
        public static Damage operator +(Damage d1, Damage d2)
        {
            d1.value += d2.value;
            return d1;
        }
        
        public static Damage operator -(Damage d1, Damage d2)
        {
            d1.value -= d2.value;
            return d1;
        }

        public static Damage operator *(Damage d1, Damage d2)
        {
            d1.value *= d2.value;
            return d1;
        }
        
        public static Damage operator *(Damage d1, float damage)
        {
            d1.value *= damage;
            return d1;
        }

        public static bool operator ==(Damage d1, Damage d2)
        {
            return d1.value == d2.value;
        }

        public static bool operator !=(Damage d1, Damage d2)
        {
            return d1.value != d2.value;
        }
    }
}