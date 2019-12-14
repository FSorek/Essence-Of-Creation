using System;
using Data.Tower;
using DataBehaviors.Tower;
using Monobehaviors.Game.Managers;
using Monobehaviors.Projectiles;
using ScriptableObjectDropdown;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackComponent : MonoBehaviour
    {
        [ScriptableObjectDropdown(typeof(TowerAttack))]public ScriptableObjectReference TowerAttack;
        private AttackController attackController;
        private TowerAttack towerAttack;

        private void Awake()
        {
            towerAttack = TowerAttack.value as TowerAttack;
            attackController = new AttackController(transform, towerAttack, WaveManager.Instance);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }

    // Shattering creates copies of the projectile on impact that jumps to nearby targets
}