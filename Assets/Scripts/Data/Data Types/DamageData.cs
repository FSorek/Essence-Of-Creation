using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data.Data_Types
{
    [CreateAssetMenu(fileName = "Damage Data", menuName = "Essence/TowerAttack/Damage Data")]
    public class DamageData : SerializedScriptableObject
    {
        [OdinSerialize]private Dictionary<DamageType, Stat> damages;
        public Dictionary<DamageType, Stat> Damages => damages;
    }
}