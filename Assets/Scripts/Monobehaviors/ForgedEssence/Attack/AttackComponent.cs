using System;
using Data.Tower;
using DataBehaviors.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackComponent : MonoBehaviour
    {
        public event Action<Projectile> OnProjectileFired;
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private TowerAttack towerAttack;
        [SerializeField] private AttackProjectileModifier projectileModifier;
        private AttackController attackController;


        private void Awake()
        {
            attackController = new AttackController(transform, towerAttack, enemiesList, projectileModifier);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }
}