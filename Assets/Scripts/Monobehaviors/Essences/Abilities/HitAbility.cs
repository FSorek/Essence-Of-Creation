using System;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public abstract class HitAbility : MonoBehaviour
    {
        [SerializeField] protected int attacksToTrigger = 3;
        protected Transform target;
        private int attackCounter;
        
        private void Awake()
        {
            attackCounter = 0;
        }

        protected bool CanApply()
        {
            return attackCounter >= attacksToTrigger;
        }
        public void AbilityProjectileFired()
        {
            if (attackCounter < attacksToTrigger)
                attackCounter++;
        }

        public void ApplyAbility(Transform target)
        {
            this.target = target;
            if(CanApply())
            {
                ApplyAbility();
                attackCounter = 0;
            }
        }

        protected abstract void ApplyAbility();
    }
}