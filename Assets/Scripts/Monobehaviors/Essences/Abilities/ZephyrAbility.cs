using Data.Game;
using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using DataBehaviors.Game.Targeting;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class ZephyrAbility : HitAbility
    {
        [SerializeField]private TransformList enemiesList;
        [SerializeField]private AttackBehaviour attack;
        [SerializeField]private int maxTargets = 2;

        protected override void ApplyAbility()
        {
            var enemiesInRange =
                RangeTargetScanner.GetTargets(transform.position, enemiesList.Items.ToArray(), attack.Range);
            var numberOfAttacks = enemiesInRange.Length > maxTargets ? maxTargets : enemiesInRange.Length;
            for (int i = 0; i < numberOfAttacks; i++)
            {
                var projectile = attack.FireProjectile(enemiesInRange[i]);
                projectile.transform.position = transform.position;
            }
        }
    }
}