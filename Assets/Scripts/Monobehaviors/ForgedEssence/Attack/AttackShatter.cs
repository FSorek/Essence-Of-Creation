using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackShatterComponent : AttackProjectileModifier
    {
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private TowerAttack attack;
        public int Shatters;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        public TowerAttack ShatterTowerAttack;

        public override void ApplyModification(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileShatter>();
            bounceComponent.Initialize(Shatters, JumpRadius, DamageReductionPerBounce, ShatterTowerAttack, enemiesList);
        }
    }
}