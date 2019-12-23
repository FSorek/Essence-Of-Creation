using Data.Data_Types;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.Game.Managers;
using Monobehaviors.Unit;
using UnityEngine;

namespace Data.Tower
{
    [CreateAssetMenu(fileName = "Area Target", menuName = "Essence/TowerAttack/Area")]
    public class AreaTowerAttack : TowerAttack
    {
        [SerializeField] private TransformList enemiesAlive;
        public float ExplosionRadius = 5f;
        public AnimationCurve DamageDistributionPercentage;
        
        public override void AttackTarget(Transform target, Damage damage)
        {
            var targetsHit = RangeTargetScanner.GetTargets(target.transform.position, enemiesAlive.Items.ToArray(),
                ExplosionRadius);
            foreach (var aoeTarget in targetsHit)
            {
                float proximity = (target.transform.position - aoeTarget.transform.position).magnitude;
                var damageScale = DamageDistributionPercentage.Evaluate((proximity / ExplosionRadius));
                var unitHealth = aoeTarget.GetComponent<UnitHealth>();
                if(unitHealth == null) return;
                
                unitHealth.TakeDamage(damage);
            }
        }
    }
    
    
}