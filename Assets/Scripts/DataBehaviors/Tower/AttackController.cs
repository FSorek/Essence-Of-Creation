using Data.Tower;
using DataBehaviors.Game.Entity.Targeting;
using DataBehaviors.Game.Utility;
using Monobehaviors.Projectiles;
using Monobehaviors.Tower.Attack;
using UnityEngine;

namespace DataBehaviors.Tower
{
    public class AttackController
    {
        private readonly Transform owner;
        private readonly TowerAttack attack;
        private readonly TransformList enemiesList;
        private readonly AttackProjectileModifier projectileModifier;
        private float lastRecordedAttackTime;
        private Transform[] targets;

        public AttackController(Transform owner, TowerAttack attack, TransformList enemiesList,
            AttackProjectileModifier projectileModifier)
        {
            this.owner = owner;
            this.attack = attack;
            this.enemiesList = enemiesList;
            this.projectileModifier = projectileModifier;
            targets = new Transform[attack.TargetLimit];
        }

        public void Tick()
        {
            if (CanAttack()) DoAttack();
        }

        public bool CanAttack()
        {
            if (GameTime.time - lastRecordedAttackTime >= attack.AttackTimer)
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
            for (int i = 0; i < attack.TargetLimit; i++)
                if (targets[i] != null)
                {
                    FireProjectile(targets[i]);
                }
        }

        private void FireProjectile(Transform target)
        {
            var projectile = GameObject.Instantiate(attack.ProjectileModel, owner.position, Quaternion.identity)
                .AddComponent<Projectile>();
            projectile.Initialize(attack, target);
            if(projectileModifier != null)
                projectileModifier.ApplyModification(projectile);
        }

        private Transform[] GetTargets()
        {
            var enemies = enemiesList.Items.ToArray();
            if (enemies.Length <= 0)
                return null;
            var availableTargets = new Transform[attack.TargetLimit];
            var enemiesInRange =
                RangeTargetScanner.GetTargets(owner.position, enemies, attack.Range);

            if (enemiesInRange.Length <= 0) return availableTargets;
            for (int i = 0; i < Mathf.Min(attack.TargetLimit, enemiesInRange.Length); i++)
                availableTargets[i] = enemiesInRange[i];

            return availableTargets;
        }
    }
}