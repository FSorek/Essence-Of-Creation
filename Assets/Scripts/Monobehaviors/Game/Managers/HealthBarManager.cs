using System;
using System.Collections.Generic;
using Data.Extensions;
using Data.ScriptableObjects.Globals;
using Monobehaviors.Pooling;
using Monobehaviors.Units;
using Monobehaviors.Units.HealthBar;
using UnityEngine;
using UnityEngine.UI;

namespace Monobehaviors.Game.Managers
{
    public class HealthBarManager : MonoBehaviour
    {
        private readonly Dictionary<Transform, HealthBar> healthBars = new Dictionary<Transform, HealthBar>();
        [SerializeField] private TransformList enemiesAlive;
        [SerializeField] private ObjectPool HealthbarPool;

        private void Awake()
        {
            enemiesAlive.OnItemAdded += AddHealthBar;
            enemiesAlive.OnItemRemoved += RemoveHealthBar;
        }

        private void RemoveHealthBar(Transform obj)
        {
            if (!healthBars.ContainsKey(obj) || healthBars[obj] == null) return;
            healthBars[obj].gameObject.ReturnToPool();
            healthBars.Remove(obj);
        }

        private void AddHealthBar(Transform obj)
        {
            if (!healthBars.ContainsKey(obj))
            {
                var unitHealth = obj.GetComponent<UnitHealth>();
                if(unitHealth == null) return;
                
                var hpBar = HealthbarPool.Get().GetComponent<HealthBar>();
                healthBars.Add(obj, hpBar);
                hpBar.SetHealth(obj.GetComponent<UnitHealth>());
                hpBar.gameObject.SetActive(true);
            }
        }
    }
}