using System;
using System.Collections.Generic;
using Data.Data_Types;
using Monobehaviors.Tower;
using UnityEngine;

namespace Monobehaviors.Unit
{
    public class UnitComponent : MonoBehaviour
    {
        public static event Action<UnitComponent> OnUnitSpawn = delegate{  };
        public static event Action<UnitComponent> OnUnitDeath = delegate{  };
        public event Action<Damage> OnTakeDamage;

        private Dictionary<StatName, Stat> stats = new Dictionary<StatName, Stat>();
        private void OnEnable()
        {
            OnUnitSpawn(this);
        }

        private void OnDisable()
        {
            OnUnitDeath(this);
        }

        public void TakeDamage(Damage damage)
        {
            //Debug.Log($"Unit took |F: {damage.Fire.Value}|W: {damage.Water.Value}|E: {damage.Earth.Value}|A: {damage.Air.Value}| damage");
            OnTakeDamage(damage);
        }

        public Stat GetStat(StatName stat)
        {
            return stats.ContainsKey(stat) ? stats[stat] : null;
        }

        public void RegisterStat(StatName statName, float value)
        {
            if(!stats.ContainsKey(statName))
                stats.Add(statName, new Stat(value));
        }
    }
}