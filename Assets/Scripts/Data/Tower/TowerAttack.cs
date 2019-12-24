using System;
using Boo.Lang;
using Data.Data_Types;
using Monobehaviors.Projectiles;
using Monobehaviors.Tower.Attack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Tower
{
    public abstract class TowerAttack : ScriptableObject
    {
        [SerializeField] protected DamageData damageData;
        [SerializeField] protected int targetLimit = 1;
        [SerializeField] protected float attackTimer = 1f;
        [SerializeField] protected float range = 40f;
        [SerializeField] protected GameObject projectileModel;
        [SerializeField] protected float projectileSpeed = 35;
        [SerializeField] protected AttackProjectileModifier projectileModifier;

        public abstract void AttackTarget(Transform target, Damage damage);

        public float Range => range;
        public float ProjectileSpeed => projectileSpeed;
        public int TargetLimit => targetLimit;
        public float AttackTimer => attackTimer;
        public GameObject ProjectileModel => projectileModel;
        public DamageData DamageData => damageData;
        public AttackProjectileModifier ProjectileModifier => projectileModifier;
    }
}