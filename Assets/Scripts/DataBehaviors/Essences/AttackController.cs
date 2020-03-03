using System;
using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using DataBehaviors.Game.Targeting;
using DataBehaviors.Game.Utility;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace DataBehaviors.Essences
{
    public class AttackController
    {
        public event Action<Projectile> OnProjectileFired = delegate {  };
        
        private readonly Transform owner;
        private readonly AttackBehaviour attackBehaviour;
        private readonly TransformList enemiesList;
        private float lastRecordedAttackTime;
        private Transform[] targets;

        public AttackController(Transform owner, AttackBehaviour attackBehaviour, TransformList enemiesList)
        {
            this.owner = owner;
            this.attackBehaviour = attackBehaviour;
            this.enemiesList = enemiesList;
            targets = new Transform[attackBehaviour.TargetLimit];
        }

        public void Tick()
        {
            if (CanAttack()) DoAttack();
        }

        public bool CanAttack()
        {
            if (GameTime.time - lastRecordedAttackTime >= attackBehaviour.AttackTimer)
            {
                targets = GetTargets();
                if (targets == null || targets[0] == null)
                    return false;
                lastRecordedAttackTime = GameTime.time;
                return true;
            }

            return false;
        }

        private void DoAttack()
        {
            for (int i = 0; i < attackBehaviour.TargetLimit; i++)
                if (targets[i] != null)
                {
                    var projectile = attackBehaviour.FireProjectile(targets[i]);
                    projectile.transform.position = owner.position;
                    OnProjectileFired(projectile);
                }
        }

        private Transform[] GetTargets()
        {
            var enemies = enemiesList.Items.ToArray();
            if (enemies.Length <= 0)
                return null;
            var availableTargets = new Transform[attackBehaviour.TargetLimit];
            var enemiesInRange =
                RangeTargetScanner.GetTargets(owner.position, enemies, attackBehaviour.Range);

            if (enemiesInRange.Length <= 0) return availableTargets;
            for (int i = 0; i < Mathf.Min(attackBehaviour.TargetLimit, enemiesInRange.Length); i++)
                availableTargets[i] = enemiesInRange[i];

            return availableTargets;
        }
    }
}