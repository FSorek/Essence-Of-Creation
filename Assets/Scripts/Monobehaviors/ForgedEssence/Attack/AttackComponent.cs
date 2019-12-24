using System;
using Data.Tower;
using DataBehaviors.Tower;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private TowerAttack towerAttack;
        private AttackController attackController;


        private void Awake()
        {
            attackController = new AttackController(transform, towerAttack, enemiesList);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }
}