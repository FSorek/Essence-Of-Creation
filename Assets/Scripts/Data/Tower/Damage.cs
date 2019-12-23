using System.Collections.Generic;
using Data.Data_Types;

namespace Data.Tower
{
    public struct Damage
    {
        private Dictionary<DamageType, Stat> damages;
        public Dictionary<DamageType, Stat> Damages => damages;
        public Damage(DamageData data)
        {
            damages = new Dictionary<DamageType, Stat>();
            foreach (var damage in data.Damages)
            {
                damages.Add(damage.Key, damage.Value);
            }
        }
        
        //public static Damage operator +(Damage d1, Damage d2)
        //{
        //    foreach (var damage in d1.damages)
        //    {
        //        if (d2.damages.ContainsKey(damage.Key))
        //            damage.Value.BaseValue += d2.damages[damage.Key].Value;
        //    }
        //    return d1;
        //}
        //
        //public static Damage operator -(Damage d1, Damage d2)
        //{
        //    foreach (var damage in d1.damages)
        //    {
        //        if (d2.damages.ContainsKey(damage.Key))
        //            damage.Value.BaseValue -= d2.damages[damage.Key].Value;
        //    }
        //    return d1;
        //}

        public static Damage operator *(Damage d1, float damagePercentage)
        {
            foreach (var damage in d1.damages.Values)
            {
                damage.BaseValue *= damagePercentage;
            }
            return d1;
        }
    }
}