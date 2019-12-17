using System.Collections.Generic;
using System.Linq;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.Game.Managers;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    
    [RequireComponent(typeof(Projectile))]
    public class ProjectileBounce : MonoBehaviour, IProjectileDeathBehaviour
    {
        private int currentBounce;
        private float damageReductionPerBounce;
        private float jumpRadius;
        private Transform currentTarget;
        private bool initialized = false;

        private Projectile projectile;
        private int totalBounces;
        
        private readonly Stack<Transform> previousTargets = new Stack<Transform>();
        private TransformList enemiesAlive;

        public void Initialize(int bounces, float damageReductionPerBounce, float jumpRadius, TransformList enemiesAlive)
        {
            totalBounces = bounces;
            currentBounce = 1;
            this.damageReductionPerBounce = damageReductionPerBounce;
            this.jumpRadius = jumpRadius;
            this.enemiesAlive = enemiesAlive;
            
            projectile = GetComponent<Projectile>();
            projectile.SetDeathBehaviour(this);
            currentTarget = projectile.Target;
            initialized = true;
        }

        public bool CanDestroy()
        {
            if(currentBounce > totalBounces || !initialized)
                return true;

            if (currentTarget != null)
                previousTargets.Push(currentTarget);
            var enemies = enemiesAlive.Items.Except(previousTargets);
            currentTarget = RangeTargetScanner.GetTargets(projectile.transform.position, enemies.ToArray(), jumpRadius).LastOrDefault();
            projectile.SetTarget(currentTarget);
            projectile.DamageScale = Mathf.Pow(damageReductionPerBounce, currentBounce);
            currentBounce++;
            
            return false;
        }
    }
}