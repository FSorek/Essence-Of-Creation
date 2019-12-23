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
        private readonly GameSettings settings;

        public WaveSpawner(ObjectPool currentPool)
        {
            this.currentPool = currentPool;
        }

        public void Spawn(Vector3 position, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                var unitEntity = currentPool.Get();
                unitEntity.transform.position = position;
                unitEntity.gameObject.SetActive(true);
            }
        }
    }
}