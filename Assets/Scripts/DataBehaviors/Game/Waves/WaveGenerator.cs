using Data.Unit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DataBehaviors.Game.Waves
{
    public class WaveGenerator 
    {
        public WaveGenerator(int seed)
        {
            Random.InitState(seed);
        }

        public UnitData Generate(int powerPoints)
        {
            var uData = LoadBaseStats();
            var pointDistribution = new int[6];
            //uData.Type = RollArmorType();
            for (int i = powerPoints; i > 0; i--) pointDistribution[Random.Range(0, pointDistribution.Length)]++;

            uData.Health += RollHealth(pointDistribution[0]);
            uData.MoveSpeed += RollMoveSpeed(pointDistribution[1]);
            uData.HealthRegen += RollHealthRegen(pointDistribution[2]);
            uData.ArmorLayers += RollArmorLayers(pointDistribution[3]);
            uData.CrystallineLayers += RollCrystallineLayers(pointDistribution[4]);
            //uData.Abilities = RollAbilities(pointDistribution[5]);

            return uData;
        }

        private int RollHealth(int v)
        {
            return v * 10;
        }

        private float RollMoveSpeed(int v)
        {
            return v * .05f;
        }

        private int RollHealthRegen(int v) // wasting some PP rolling
        {
            return Mathf.RoundToInt(v * .1f);
        }

        private int RollArmorLayers(int v) // wasting some PP rolling
        {
            return Mathf.FloorToInt(v / 2f);
        }

        private int RollCrystallineLayers(int v) // wasting some PP rolling
        {
            return Mathf.FloorToInt(v / 4f);
        }

        //private UnitAbility[] RollAbilities(int v)
        //{
        //    return new UnitAbility[0];
        //}

        private UnitData LoadBaseStats()
        {
            var uData = new UnitData();

            uData.Health = 20;
            uData.MoveSpeed = 4f;
            uData.HealthRegen = 0;
            //uData.Type = ArmorType.None;
            uData.ArmorLayers = 0;
            uData.CrystallineLayers = 0;
            //uData.Abilities = null;

            return uData;
        }
    }
}