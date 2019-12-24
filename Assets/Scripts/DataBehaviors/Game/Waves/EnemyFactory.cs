using Data.Data_Types;
using Monobehaviors.Pooling;
using Monobehaviors.Unit;
using UnityEngine;

namespace DataBehaviors.Game.Waves
{
    public class EnemyFactory
    {
        private ObjectPool enemyPool;

        public EnemyFactory(int seed, ObjectPool enemyPool)
        {
            Random.InitState(seed);
            this.enemyPool = enemyPool;
        }

        public GameObject CreateEnemy(int power, StatConversionRates conversionRates)
        {
            var enemy = enemyPool.Get();
            var stats = enemy.GetComponent<StatController>();

            int health = Random.Range(conversionRates.EffectiveHealthRange.x, conversionRates.EffectiveHealthRange.y);
            int armorToughness = Random.Range(conversionRates.ArmorToughnessRange.x, conversionRates.ArmorToughnessRange.y);
            float movementSpeed = Random.Range(conversionRates.MovementSpeedRange.x, conversionRates.MovementSpeedRange.y);
            float healthRegen = Random.Range(conversionRates.HealthRegenRange.x, conversionRates.HealthRegenRange.y);

            float healthWeight = health * conversionRates.EffectiveHealthRatio;
            float armorWeight = armorToughness * conversionRates.ArmorToughnessRatio;
            float hpRegenWeight = healthRegen * conversionRates.HealthRegenRatio;

            float moveSpeedAverage = (conversionRates.MovementSpeedRange.y + conversionRates.MovementSpeedRange.x) / 2;
            float moveSpeedDeviation = movementSpeed - moveSpeedAverage;
            float maxMoveSpeedDeviation = conversionRates.MovementSpeedRange.y - moveSpeedAverage;
            float moveSpeedModifier = 1 + (moveSpeedDeviation / maxMoveSpeedDeviation * -1f * conversionRates.MovementSpeedMaxBonus);
                
            float currentPower =   healthWeight
                                 + armorWeight
                                 + hpRegenWeight;
            
            health = Mathf.CeilToInt(RescaleStat(power, healthWeight, currentPower) / conversionRates.EffectiveHealthRatio * moveSpeedModifier);
            armorToughness = Mathf.CeilToInt(RescaleStat(power, armorWeight, currentPower) / conversionRates.ArmorToughnessRatio * moveSpeedModifier);
            healthRegen = RescaleStat(power, hpRegenWeight, currentPower) / conversionRates.HealthRegenRatio * moveSpeedModifier;

            stats.SetStat(StatName.MovementSpeed, movementSpeed);
            stats.SetStat(StatName.HealthPool, health);
            stats.SetStat(StatName.ArmorToughness, armorToughness);
            stats.SetStat(StatName.HealthRegeneration, healthRegen);

            //stats.SetStat(StatName.CrystallineLayers, crystallineLayers);
            return enemy;
        }

        private float RescaleStat(int power, float weight, float currentPower)
        {
            return power * weight / currentPower;
        }
    }
}