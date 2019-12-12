using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    [RequireComponent(typeof(AttackComponent))]
    public class AttackShatterComponent : MonoBehaviour
    {
        public int Shatters;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        public TowerAttack ShatterTowerAttack;
        
        private AttackComponent attack;
        private void Awake()
        {
            attack = GetComponent<AttackComponent>();
            attack.OnProjectileFired += AttackOnProjectileFired;
        }

        private void AttackOnProjectileFired(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileShatter>();
            bounceComponent.Initialize(Shatters, JumpRadius, DamageReductionPerBounce, ShatterTowerAttack);
        }
    }
}