using Data.Data_Types;
using UnityEngine;

namespace Data.Tower
{
    public abstract class TowerAttack : ScriptableObject
    {
        [SerializeField] protected int targetLimit = 1;
        [SerializeField] protected Damage baseDamage;
        [SerializeField] protected float attackTimer = 1f;
        [SerializeField] protected float range = 40f;
        [SerializeField] protected GameObject projectileModel;
        [SerializeField] protected float projectileSpeed = 35;

        public abstract void AttackTarget(Transform target, Damage damage);

        public float Range => range;
        public float ProjectileSpeed => projectileSpeed;
        public int TargetLimit => targetLimit;
        public float AttackTimer => attackTimer;
        public GameObject ProjectileModel => projectileModel;
        public Damage Damage => baseDamage;
    }
}