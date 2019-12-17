using Data.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackBounce : AttackProjectileModifier
    {
        [SerializeField] private TransformList enemiesList;
        public AttackComponent attack;
        public int Bounces;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;

        public override void ApplyModification(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileBounce>();
            bounceComponent.Initialize(Bounces, DamageReductionPerBounce, JumpRadius, enemiesList);
        }
    }
}