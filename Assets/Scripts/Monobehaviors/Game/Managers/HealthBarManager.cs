using System.Collections.Generic;
using Data.Extensions;
using Monobehaviors.Pooling;
using Monobehaviors.Unit;
using Monobehaviors.Unit.HealthBar;
using UnityEngine;
using UnityEngine.UI;

namespace Monobehaviors.Game.Managers
{
    public class HealthBarManager : MonoBehaviour
    {
        private readonly Dictionary<UnitComponent, HealthBar> healthBars = new Dictionary<UnitComponent, HealthBar>();
        [SerializeField] private ObjectPool Pool;

        private void Awake()
        {
            UnitComponent.OnUnitSpawn += AddHealthBar;
            UnitComponent.OnUnitDeath += RemoveHealthBar;
        }

        private void RemoveHealthBar(UnitComponent obj)
        {
            if (!healthBars.ContainsKey(obj) || healthBars[obj] == null) return;
            healthBars[obj].gameObject.ReturnToPool();
            healthBars.Remove(obj);
        }

        private void AddHealthBar(UnitComponent obj)
        {
            if (!healthBars.ContainsKey(obj))
            {
                var hpBar = Pool.Get().GetComponent<HealthBar>();
                healthBars.Add(obj, hpBar);
                hpBar.SetHealth(obj);
                hpBar.gameObject.SetActive(obj.gameObject.activeSelf);
            }
        }
    }
}