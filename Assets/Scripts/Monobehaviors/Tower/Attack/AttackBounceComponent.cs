using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackBounceComponent : MonoBehaviour
    {
        [SerializeField] private TowerAttack attack;
        public int Bounces;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        
        private void Awake()
        {
            attack.OnProjectileFired += AttackOnProjectileFired;
        }

        private void AttackOnProjectileFired(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileBounce>();
            bounceComponent.Initialize(Bounces, DamageReductionPerBounce, JumpRadius);
        }
    }
}