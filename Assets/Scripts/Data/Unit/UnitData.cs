using System;
using Data.Data_Types;

namespace Data.Unit
{
    [Serializable]
    public class UnitData
    {
        public Stat ArmorLayers;
        public Stat CrystallineLayers;
        public Stat Health;
        public Stat HealthRegen;
        public Stat MoveSpeed;

        public ArmorType Type;

        //public UnitAbility[] Abilities;
    }
}