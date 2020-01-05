using System.Collections.Generic;
using Data.Data_Types;
using Data.ScriptableObjects.Game;

namespace Data.Game
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
                var stat = new Stat(damage.Value);
                damages.Add(damage.Key, stat);
            }
        }

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