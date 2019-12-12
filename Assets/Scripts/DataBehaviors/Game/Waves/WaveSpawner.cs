using Data.Game;
using Data.Interfaces.Game.Waves;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.Game.Managers;
using Monobehaviors.Pooling;
using UnityEngine;

namespace DataBehaviors.Game.Waves
{
    public class WaveSpawner : IWaveSpawner
    {
        private readonly ObjectPool currentPool;
        private readonly WaveSettings settings;

        public WaveSpawner(WaveSettings settings, ObjectPool currentPool)
        {
            this.settings = settings;
            this.currentPool = currentPool;
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
            while (RangeTargetScanner.GetTargets(newPosition, WaveManager.Instance.UnitsAlive, 1f).Length > 0
            ) newPosition = newPosition + new Vector3(0, 0, -1f);

            return newPosition;
        }
    }
}