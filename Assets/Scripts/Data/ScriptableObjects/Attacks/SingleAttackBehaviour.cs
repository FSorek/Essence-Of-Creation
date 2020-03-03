using System.Collections.Generic;
using Data.Game;
using Monobehaviors.Essences.Attacks;
using Monobehaviors.Units;
using UnityEngine;

namespace Data.ScriptableObjects.Attacks
{
    [CreateAssetMenu(fileName = "Single Target", menuName = "Essence/TowerAttack/Single")]
    public class SingleAttackBehaviour : AttackBehaviour
    {
        public override void AttackTarget(Transform target, Damage damage, IEnumerable<HitAbility> hitAbilities)
        {
            var unitHealth = target.GetComponent<UnitHealth>();
            if(unitHealth == null) return;

            if (hitAbilities != null)
            {
                foreach (var hitAbility in hitAbilities)
                {
                    hitAbility.ApplyAbility(target);
                }
            }
            unitHealth.TakeDamage(damage);
        }
    }
}