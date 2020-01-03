using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class EffectController : MonoBehaviour
    {
        private readonly List<IAbilityStatus> abilityStatuses = new List<IAbilityStatus>();
        public void ApplyStatus(IAbilityStatus status)
        {
            status.EffectApplied(this);
            abilityStatuses.Add(status);
        }

        private void Update()
        {
            for (int i = 0; i < abilityStatuses.Count; i++)
            {
                var abilityStatus = abilityStatuses[i];
                
                abilityStatus.Tick(this);
                if (abilityStatus.Expired)
                    abilityStatuses.Remove(abilityStatus);
            }
        }

        private void OnDisable()
        {
            abilityStatuses.Clear();
        }
    }
}