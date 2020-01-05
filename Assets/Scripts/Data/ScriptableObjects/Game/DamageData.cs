using System.Collections.Generic;
using Data.Data_Types;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data.ScriptableObjects.Game
{
    [CreateAssetMenu(fileName = "Damage Data", menuName = "Essence/TowerAttack/Damage Data")]
    public class DamageData : SerializedScriptableObject
    {
        [OdinSerialize]private Dictionary<DamageType, int> damages;
        public Dictionary<DamageType, int> Damages => damages;
    }
}