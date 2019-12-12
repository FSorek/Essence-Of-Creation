using System;
using System.Collections.Generic;
using Data.Data_Types;
using Data.Tower;
using Monobehaviors.Tower.Attack;
using Monobehaviors.Unit;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Transform> OnTargetHit = delegate {  };

        public float DamageScale
        {
            get => damageScale;
            set => damageScale = value;
        }

        private TowerAttack attackBehaviour;
        private bool initialized;
        private Transform target;
        private SimpleMove move;
        private IProjectileDeathBehaviour DeathBehaviour;
        private Damage damage;
        private float damageScale;
        public TowerAttack AttackBehaviour => attackBehaviour;
        public Transform Target => target;

        private void Update()
        {
            if(!initialized)
                return;
            if(target == null)
                Destroy(gameObject);

            var dir = target.position - transform.position;
            float distThisFrame = attackBehaviour.ProjectileSpeed * Time.deltaTime;
            
            if (dir.magnitude <= distThisFrame)
            {
                attackBehaviour.AttackTarget(target, CalculateDamage());
                OnTargetHit(target);
                Die();
            }
            if(target != null)
                move.Move(target.transform.position, attackBehaviour.ProjectileSpeed);
        }

        private Damage CalculateDamage()
        {
            return damage * damageScale;
        }

        public void Initialize(TowerAttack attackBehaviour, Transform target)
        {
            this.target = target;
            this.attackBehaviour = attackBehaviour;
            move = new SimpleMove(transform);
            DeathBehaviour = new ProjectileSimpleDeath();
            damage = attackBehaviour.Damage;
            damageScale = 1f;
            initialized = true;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void Die()
        {
            if(DeathBehaviour.CanDestroy() || target == null)
                Destroy(gameObject);
        }

        public void SetDeathBehaviour(IProjectileDeathBehaviour behaviour)
        {
            DeathBehaviour = behaviour;
        }
    }
}