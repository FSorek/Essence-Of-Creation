using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    [CreateAssetMenu(fileName = "Attack Shatter", menuName = "Essence/TowerAttack/Attack Shatter")]

    public class AttackShatter : AttackProjectileModifier
    {
        [SerializeField] private TransformList enemiesList;
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