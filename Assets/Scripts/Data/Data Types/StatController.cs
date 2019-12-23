using System;
using System.Collections.Generic;
using Monobehaviors.Unit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Data_Types
{
    public class StatController : SerializedMonoBehaviour
    {
        [SerializeField]private Dictionary<StatName, Stat> stats = new Dictionary<StatName, Stat>();

        public Stat GetStat(StatName stat)
        {
            return stats.ContainsKey(stat) ? stats[stat] : null;
        }

        public void SetStat(StatName type, Stat stat)
        {
            if (!stats.ContainsKey(type))
                stats.Add(type, stat);
            else
                stats[type] = stat;
        }
    }
}