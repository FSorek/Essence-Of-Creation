﻿using Data.Data_Types;
using Monobehaviors.Unit;
using UnityEngine;

namespace Data.Tower
{
    [CreateAssetMenu(fileName = "Single Target", menuName = "Essence/TowerAttack/Single")]
    public class SingleTowerAttack : TowerAttack
    {
        public override void AttackTarget(Transform target, Damage damage)
        {
            var unitHealth = target.GetComponent<UnitHealth>();
            if(unitHealth == null) return;
            
            unitHealth.TakeDamage(damage);
        }
    }
}