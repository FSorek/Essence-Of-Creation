using UnityEngine;

namespace Data.Data_Types
{
    [CreateAssetMenu(fileName = "Armor Type", menuName = "Essence/Entity/Armor Type")]

    public class ArmorType : ScriptableObject
    {
        public float FireResistance;
        public float WaterResistance;
        public float EarthResistance;
        public float AirResistance;
    }
}