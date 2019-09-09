using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private ObjectPool Pool;
    [SerializeField] private HealthBar healthBarPrefab;

    private Dictionary<Unit, HealthBar> healthBars = new Dictionary<Unit, HealthBar>();

    private void Awake()
    {
        WaveManager.OnSpawn += AddHealthBar;
        Unit.OnUnitDeath += RemoveHealthBar;

    }

    private void RemoveHealthBar(Unit obj)
    {
        if (healthBars.ContainsKey(obj))
        {
            healthBars[obj].gameObject.ReturnToPool();
            healthBars.Remove(obj);
        }
    }

    private void AddHealthBar(Unit obj)
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