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

        public GameObject CreateEnemy(int hp, float moveSpeed, int toughness, float hpRegen, int crystal)
        {
            var enemy = enemyPool.Get();
            var stats = enemy.GetComponent<StatController>();
//
            var health = new Stat(hp);
            var movementSpeed = new Stat(moveSpeed);
            var armorToughness = new Stat(toughness);
            var healthRegen = new Stat(hpRegen);
            var crystallineLayers = new Stat(crystal);
//
            stats.SetStat(StatName.HealthPool, health);
            stats.SetStat(StatName.MovementSpeed, movementSpeed);
            stats.SetStat(StatName.ArmorToughness, armorToughness);
            stats.SetStat(StatName.HealthRegeneration, healthRegen);
            stats.SetStat(StatName.CrystallineLayers, crystallineLayers);

            return enemy;
        }
    }
}