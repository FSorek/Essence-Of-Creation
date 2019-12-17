using UnityEngine;

namespace Data.Data_Types
{
    public class EarthDamage : Damage
    {
        public EarthDamage(int baseValue) : base(baseValue)
        {
        }

        public override int GetDamageToArmor(ArmorType armorType)
        {
            return armorType == null ? 1 : Mathf.RoundToInt(value * armorType.EarthResistance);
        }
    }
}