using System.Collections;
using Data.Game;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.Pooling;
using UnityEngine;

namespace DataBehaviors.Game.Waves
{
    public class WaveSpawner
    {
        private readonly ObjectPool currentPool;
        private readonly TransformList enemiesAlive;
        private readonly GameSettings settings;

        public WaveSpawner(ObjectPool currentPool, TransformList enemiesAlive)
        {
            this.currentPool = currentPool;
            this.enemiesAlive = enemiesAlive;
        }

        public void Spawn(Vector3 position, int amount = 1)
        {
            //var model = settings.ModelPool[Random.Range(0, settings.ModelPool.Length)];
            for (int i = 0; i < amount; i++)
            {
                var unitEntity = currentPool.Get();
                unitEntity.transform.position = AdjustPosition(position, amount);
                unitEntity.gameObject.SetActive(true);
            }
        }

        private Vector3 AdjustPosition(Vector3 position, int amount)
        {
            if (amount <= 1) return position;

            var newPosition = position;
            while (RangeTargetScanner.GetTargets(newPosition, enemiesAlive.Items.ToArray(), 1f).Length > 0
            ) newPosition = newPosition + new Vector3(0, 0, -1f);

            return newPosition;
        }
        
        public IEnumerator SpawnWave(Vector3 spawnPosition)
        {
            //spawn
            yield return null;
        }
    }
}