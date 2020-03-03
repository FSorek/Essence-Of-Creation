using Data.ScriptableObjects.Globals;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Data.ScriptableObjects.ForgedEssence
{
    [CreateAssetMenu(fileName = "Attack Bounce", menuName = "Essence/TowerAttack/Attack Bounce")]

    public class AttackBounce : AttackProjectileModifier
    {
        [SerializeField] private TransformList enemiesList;
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