using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Data_Types
{
    [Serializable]
    public struct Damage
    {
        public Stat Fire => fire;
        public Stat Air => air;
        public Stat Water => water;
        public Stat Earth => earth;
        
        [SerializeField] private Stat fire;
        [SerializeField] private Stat air;
        [SerializeField] private Stat water;
        [SerializeField] private Stat earth;

        public Damage(int fire, int air, int water, int earth)
        {
            this.fire = fire;
            this.air = air;
            this.water = water;
            this.earth = earth;
        }

        public Elements GetDominatingDamage()
        {
            var dict = new Dictionary<Elements, Stat>
            {
                {Elements.Fire, Fire},
                {Elements.Water, Water},
                {Elements.Earth, Earth},
                {Elements.Air, Air}
            };
            int highestVal = Mathf.Max(Fire, Water, Earth, Air);
            return dict.Where(e => e.Value == highestVal).First().Key;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Damage)) return false;

            var damage = (Damage) obj;
            return Fire.BaseValue == damage.Fire.BaseValue && Water.BaseValue == damage.Water.BaseValue &&
                   Earth.BaseValue == damage.Earth.BaseValue && Air.BaseValue == damage.Air.BaseValue;
        }

        public override int GetHashCode()
        {
            int hashCode = 1539283294;
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Earth);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Water);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Air);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Fire);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Fire);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Air);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Water);
            hashCode = hashCode * -1521134295 + EqualityComparer<Stat>.Default.GetHashCode(Earth);
            return hashCode;
        }

        public static Damage operator +(Damage d1, Damage d2)
        {
            d1.fire += d2.Fire;
            d1.air += d2.Air;
            d1.water += d2.Water;
            d1.earth += d2.Earth;

            return d1;
        }
        
        public static Damage operator -(Damage d1, Damage d2)
        {
            d1.fire -= d2.Fire;
            d1.air -= d2.Air;
            d1.water -= d2.Water;
            d1.earth -= d2.Earth;

            return d1;
        }

        public static Damage operator *(Damage d1, Damage d2)
        {
            d1.fire *= d2.Fire;
            d1.air *= d2.Air;
            d1.water *= d2.Water;
            d1.earth *= d2.Earth;

            return d1;
        }
        
        public static Damage operator *(Damage d1, float damage)
        {
            d1.fire *= damage;
            d1.air *= damage;
            d1.water *= damage;
            d1.earth *= damage;

            return d1;
        }

        public static bool operator ==(Damage d1, Damage d2)
        {
            return d1.Fire.BaseValue == d2.Fire.BaseValue && d1.Water.BaseValue == d2.Water.BaseValue &&
                   d1.Earth.BaseValue == d2.Earth.BaseValue && d1.Air.BaseValue == d2.Air.BaseValue;
        }

        public static bool operator !=(Damage d1, Damage d2)
        {
            return d1.Fire.BaseValue != d2.Fire.BaseValue || d1.Water.BaseValue != d2.Water.BaseValue ||
                   d1.Earth.BaseValue != d2.Earth.BaseValue || d1.Air.BaseValue != d2.Air.BaseValue;
        }
    }
}