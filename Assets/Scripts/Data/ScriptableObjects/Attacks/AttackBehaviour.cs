using System.Collections.Generic;
using Data.Game;
using Data.ScriptableObjects.ForgedEssence;
using Data.ScriptableObjects.Game;
using Monobehaviors.Essences.Attacks;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Data.ScriptableObjects.Attacks
{
    public abstract class AttackBehaviour : ScriptableObject
    {
        [SerializeField] protected DamageData damageData;
        [SerializeField] protected int targetLimit = 1;
        [SerializeField] protected float attackTimer = 1f;
        [SerializeField] protected float range = 40f;
        [SerializeField] protected GameObject projectileModel;
        [SerializeField] protected float projectileSpeed = 35;
        [SerializeField] protected AttackProjectileModifier projectileModifier;

        public abstract void AttackTarget(Transform target, Damage damage, IEnumerable<HitAbility> hitAbilities);
        
        public Projectile FireProjectile(Transform target)
        {
            var projectile = GameObject.Instantiate(projectileModel, Vector3.zero, Quaternion.identity)
                .AddComponent<Projectile>();
            projectile.Initialize(this, target);
            if(projectileModifier != null)
                projectileModifier.ApplyModification(projectile);

            return projectile;
        }

        public float Range => range;
        public float ProjectileSpeed => projectileSpeed;
        public int TargetLimit => targetLimit;
        public float AttackTimer => attackTimer;
        public GameObject ProjectileModel => projectileModel;
        public DamageData DamageData => damageData;
    }
}