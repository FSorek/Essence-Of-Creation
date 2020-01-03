using System;
using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using DataBehaviors.Essences;
using Monobehaviors.Game;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private AttackBehaviour attackBehaviour;
        [SerializeField] private HitAbility[] hitAbilities;
        private AttackController attackController;
        private void Awake()
        {
            attackController = new AttackController(transform, attackBehaviour, enemiesList);
            attackController.OnProjectileFired += AttackControllerOnProjectileFired;
        }

        private void AttackControllerOnProjectileFired(Projectile projectile)
        {
            projectile.AttachAbility(hitAbilities);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }
}