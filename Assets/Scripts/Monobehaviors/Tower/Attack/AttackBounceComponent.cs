using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    [RequireComponent(typeof(AttackComponent))]
    public class AttackBounceComponent : MonoBehaviour
    {
        public int Bounces;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        
        private AttackComponent attack;
        private void Awake()
        {
            attack = GetComponent<AttackComponent>();
            attack.OnProjectileFired += AttackOnProjectileFired;
        }

        private void AttackOnProjectileFired(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileBounce>();
            bounceComponent.Initialize(Bounces, DamageReductionPerBounce, JumpRadius);
        }
    }
}