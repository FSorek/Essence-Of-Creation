using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackShatterComponent : MonoBehaviour
    {
        [SerializeField] private TowerAttack attack;
        public int Shatters;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        public TowerAttack ShatterTowerAttack;
        private void Awake()
        {
            attack.OnProjectileFired += AttackOnProjectileFired;
        }

        private void AttackOnProjectileFired(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileShatter>();
            bounceComponent.Initialize(Shatters, JumpRadius, DamageReductionPerBounce, ShatterTowerAttack);
        }
    }
}