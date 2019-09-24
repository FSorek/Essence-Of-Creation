using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private ObjectPool Pool;

    private Dictionary<ITakeDamage, HealthBar> healthBars = new Dictionary<ITakeDamage, HealthBar>();

    private void Awake()
    {
        Unit.OnUnitSpawn += AddHealthBar;
        Unit.OnUnitDeath += RemoveHealthBar;
    }

    private void RemoveHealthBar(ITakeDamage obj)
    {
        if (healthBars.ContainsKey(obj))
        {
            healthBars[obj].gameObject.ReturnToPool();
            healthBars.Remove(obj);
        }
    }

    private void AddHealthBar(ITakeDamage obj)
    {
        if (!healthBars.ContainsKey(obj))
        {
            var hpBar = Pool.Get().GetComponent<HealthBar>();
            healthBars.Add(obj, hpBar);
            hpBar.SetHealth(obj);
            hpBar.gameObject.SetActive(true);
        }
    }
}