﻿using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ScriptableObjects.Game
{
    [CreateAssetMenu(fileName = "Damage Type", menuName = "Essence/TowerAttack/Damage Type")]
    public class DamageType : SerializedScriptableObject
    {
        public Dictionary<ArmorType, float> ArmorMultipliers = new Dictionary<ArmorType, float>();
        // prolly add a sprite :)
    }
}