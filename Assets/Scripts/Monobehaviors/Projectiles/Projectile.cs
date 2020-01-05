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
using JetBrains.Annotations;
using Monobehaviors.Essences.Attacks;
using Monobehaviors.Units;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private List<HitAbility> hitAbilities = new List<HitAbility>();
        private AttackBehaviour attackBehaviour;
        private IProjectileDeathBehaviour deathBehaviour;
        private bool initialized;
        private Transform target;
        private IMover move;
        private float damagePercentage;
        private Damage damage;
        public AttackBehaviour AttackBehaviour => attackBehaviour;
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
            float distThisFrame = attackBehaviour.ProjectileSpeed * Time.deltaTime;
            
            if (dir.magnitude <= distThisFrame)
            {
                attackBehaviour.AttackTarget(target, damage * damagePercentage, hitAbilities);
                Die();
            }
            if(target != null)
                move.Move(target.transform.position, attackBehaviour.ProjectileSpeed);
        }

        public void Initialize(AttackBehaviour attackBehaviour, Transform target)
        {
            this.target = target;
            this.attackBehaviour = attackBehaviour;
            move = new SimpleMove(transform);
            deathBehaviour = new ProjectileSimpleDeath();
            damagePercentage = 1f;
            damage = new Damage(attackBehaviour.DamageData);
            initialized = true;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        private void Die()
        {
            if(deathBehaviour.CanDestroy() || target == null)
                Destroy(gameObject);
        }

        public void SetDeathBehaviour(IProjectileDeathBehaviour behaviour)
        {
            deathBehaviour = behaviour;
        }

        public void AttachAbility(HitAbility ability)
        {
            hitAbilities.Add(ability);
        }
        
        public void AttachAbility([NotNull] IEnumerable<HitAbility> abilities)
        {
            if(hitAbilities == null)
                hitAbilities = new List<HitAbility>(abilities);
            else
            {
                foreach (var ability in abilities)
                {
                    AttachAbility(ability);
                }
            }
        }
    }
}