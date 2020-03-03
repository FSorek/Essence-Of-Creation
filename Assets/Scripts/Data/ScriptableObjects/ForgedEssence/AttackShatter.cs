using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Data.ScriptableObjects.ForgedEssence
{
    [CreateAssetMenu(fileName = "Attack Shatter", menuName = "Essence/TowerAttack/Attack Shatter")]

    public class AttackShatter : AttackProjectileModifier
    {
        [SerializeField] private TransformList enemiesList;
        public int Shatters;
        [Range(0,1f)]
        public float DamageReductionPerBounce;
        public float JumpRadius;
        public AttackBehaviour shatterAttackBehaviour;

        public override void ApplyModification(Projectile projectile)
        {
            var bounceComponent = projectile.gameObject.AddComponent<ProjectileShatter>();
            bounceComponent.Initialize(Shatters, JumpRadius, DamageReductionPerBounce, shatterAttackBehaviour, enemiesList);
        }
    }
}