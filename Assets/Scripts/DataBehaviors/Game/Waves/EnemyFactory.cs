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

            float movementSpeed = Random.Range(conversionRates.MovementSpeedRange.x, conversionRates.MovementSpeedRange.y);
            int effectiveHealth = Random.Range(conversionRates.EffectiveHealthRange.x, conversionRates.EffectiveHealthRange.y);
            float healthRegen = Random.Range(conversionRates.HealthRegenRange.x, conversionRates.HealthRegenRange.y);

            float movementWeight = movementSpeed * conversionRates.MovementSpeedRatio;
            float healthWeight = effectiveHealth * conversionRates.EffectiveHealthRatio;
            float hpRegenWeight = healthRegen * conversionRates.HealthRegenRatio;

            float currentPower =   movementWeight
                                 + healthWeight
                                 + hpRegenWeight;
            
            movementSpeed = RescaleStat(power, movementWeight, currentPower) / conversionRates.MovementSpeedRatio;
            effectiveHealth = Mathf.CeilToInt(RescaleStat(power, healthWeight, currentPower) / conversionRates.EffectiveHealthRatio);
            healthRegen = RescaleStat(power, hpRegenWeight, currentPower) / conversionRates.HealthRegenRatio;
            
            stats.SetStat(StatName.MovementSpeed, movementSpeed);
            stats.SetStat(StatName.HealthPool, effectiveHealth);
            //stats.SetStat(StatName.ArmorToughness, armorToughness);
            stats.SetStat(StatName.HealthRegeneration, healthRegen);

            //stats.SetStat(StatName.CrystallineLayers, crystallineLayers);
            return enemy;
        }

        private float RescaleStat(int power, float ratio, float currentPower)
        {
            return power * ratio / currentPower;
        }
    }
}