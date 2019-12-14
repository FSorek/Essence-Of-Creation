using System;
using Data.Data_Types;
using Monobehaviors.Projectiles;
using Monobehaviors.Tower.Attack;
using UnityEngine;

namespace Data.Tower
{
    public abstract class TowerAttack : ScriptableObject
    {
        public event Action<Projectile> OnProjectileFired = delegate {  };
        
        [SerializeField] protected int targetLimit = 1;
        [SerializeField] protected Damage baseDamage;
        [SerializeField] protected float attackTimer = 1f;
        [SerializeField] protected float range = 40f;
        [SerializeField] protected GameObject projectileModel;
        [SerializeField] protected float projectileSpeed = 35;

        public abstract void AttackTarget(Transform target, Damage damage);

        public void CreateProjectile(Vector3 spawnPosition, Transform target)
        {
            var projectile = GameObject.Instantiate(ProjectileModel, spawnPosition, Quaternion.identity).AddComponent<Projectile>();
            projectile.Initialize(this, target);
            OnProjectileFired(projectile);
        }

        public float Range => range;
        public float ProjectileSpeed => projectileSpeed;
        public int TargetLimit => targetLimit;
        public float AttackTimer => attackTimer;
        public GameObject ProjectileModel => projectileModel;
        public Damage Damage => baseDamage;
    }
}