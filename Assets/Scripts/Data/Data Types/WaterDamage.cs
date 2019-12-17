using UnityEngine;

namespace Data.Data_Types
{
    public class WaterDamage : Damage
    {
        public WaterDamage(int baseValue) : base(baseValue)
        {
        }

        public override int GetDamageToArmor(ArmorType armorType)
        {
            return armorType == null ? 1 :  Mathf.RoundToInt(value * armorType.WaterResistance);
        }
    }
}