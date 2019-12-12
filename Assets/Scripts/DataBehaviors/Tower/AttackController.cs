using System.Linq;
using Data.Interfaces.Game.Waves;
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
        private float lastRecordedAttackTime;
        private readonly Transform owner;
        private readonly TowerAttack attack;
        private Transform[] targets;
        private readonly IWaveManager waveManager;

        public AttackController(Transform owner, TowerAttack attack, IWaveManager waveManager)
        {
            this.owner = owner;
            this.attack = attack;
            this.waveManager = waveManager;
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
                    CreateProjectile(targets[i]);
                }
        }

        private void CreateProjectile(Transform target)
        {
            var projectile = GameObject.Instantiate(attack.ProjectileModel, owner.position, Quaternion.identity).AddComponent<Projectile>();
            projectile.Initialize(attack, target);
            owner.GetComponent<AttackComponent>().FireOnProjectileFired(projectile);
        }

        protected Transform[] GetTargets()
        {
            var enemies = waveManager.UnitsAlive;
            if (enemies.Length <= 0)
                return null;
            var availableTargets = new Transform[attack.TargetLimit];
            var enemiesInRange =
                RangeTargetScanner.GetTargets(owner.transform.position, enemies, attack.Range);

            if (enemiesInRange.Length <= 0) return availableTargets;
            for (int i = 0; i < Mathf.Min(attack.TargetLimit, enemiesInRange.Length); i++)
                availableTargets[i] = enemiesInRange[i];

            return availableTargets;
        }
    }
}