﻿using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Tower;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.Game.Managers;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileShatter : MonoBehaviour, IProjectileDeathBehaviour
    {
        private int shatterAmount;
        private float damageReductionOnShatter;
        private float jumpRadius;
        private Transform currentTarget;
        private bool initialized = false;
        private TowerAttack shatterTowerAttack;

        private Projectile projectile;
        
        private readonly Stack<Transform> previousTargets = new Stack<Transform>();
        public void Initialize(int shatterAmount, float jumpRadius, float damageReductionOnShatter, TowerAttack shatterTowerAttack)
        {
            this.shatterAmount = shatterAmount;
            this.damageReductionOnShatter = damageReductionOnShatter;
            this.jumpRadius = jumpRadius;
            this.shatterTowerAttack = shatterTowerAttack;
            
            projectile = GetComponent<Projectile>();
            projectile.SetDeathBehaviour(this);
            currentTarget = projectile.Target;
            
            initialized = true;
        }

        public bool CanDestroy()
        {
            if(!initialized)
                return true;
            
            if (currentTarget != null)
                previousTargets.Push(currentTarget);
            
            var enemies = WaveManager.Instance.UnitsAlive.Except(previousTargets);
            var availableTargets = RangeTargetScanner.GetTargets(projectile.transform.position, enemies.ToArray(), jumpRadius);
            var shatters = availableTargets.Length >= shatterAmount ? shatterAmount : availableTargets.Length;
            
            for (int i = 0; i < shatters; i++)
            {
                // spawn new projectiles, to-do: pool
                var proj = Instantiate(projectile.AttackBehaviour.ProjectileModel, transform.position, Quaternion.identity).AddComponent<Projectile>();
                proj.Initialize(shatterTowerAttack, availableTargets[i]);
                proj.DamageScale = damageReductionOnShatter;
            }
            
            return true;
        }
    }
}