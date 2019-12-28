using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Game;
using Data.Interfaces.Game;
using Data.Interfaces.Projectiles;
using Data.ScriptableObjects.Attacks;
using DataBehaviors.Game.Movements;
using DataBehaviors.Projectiles;
using Monobehaviors.Units;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public class Projectile : MonoBehaviour
    {

        private AttackBehaviour attackBehaviourBehaviour;
        private bool initialized;
        private Transform target;
        private IMover move;
        private IProjectileDeathBehaviour deathBehaviour;
        private float damagePercentage;
        private Damage damage;
        public AttackBehaviour AttackBehaviourBehaviour => attackBehaviourBehaviour;
        public Transform Target => target;
        public float DamageScale
        {
            set => damagePercentage = value;
        }

        private void Update()
        {
            if(!initialized)
                return;
            if(target == null)
                Destroy(gameObject);

            var dir = target.position - transform.position;
            float distThisFrame = attackBehaviourBehaviour.ProjectileSpeed * Time.deltaTime;
            
            if (dir.magnitude <= distThisFrame)
            {
                attackBehaviourBehaviour.AttackTarget(target, damage * damagePercentage);
                Die();
            }
            if(target != null)
                move.Move(target.transform.position, attackBehaviourBehaviour.ProjectileSpeed);
        }

        public void Initialize(AttackBehaviour attackBehaviourBehaviour, Transform target)
        {
            this.target = target;
            this.attackBehaviourBehaviour = attackBehaviourBehaviour;
            move = new SimpleMove(transform);
            deathBehaviour = new ProjectileSimpleDeath();
            damagePercentage = 1f;
            damage = new Damage(attackBehaviourBehaviour.DamageData);
            initialized = true;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void Die()
        {
            if(deathBehaviour.CanDestroy() || target == null)
                Destroy(gameObject);
        }

        public void SetDeathBehaviour(IProjectileDeathBehaviour behaviour)
        {
            deathBehaviour = behaviour;
        }
    }
}