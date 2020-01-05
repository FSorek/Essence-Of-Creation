using Data.Game;
using DataBehaviors.Game.Utility;
using Monobehaviors.Units;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class DamageOverTimeStatus : IAbilityStatus
    {
        private const float TickFrequency = .5f;
        private readonly Damage damage;
        private readonly float duration;
        private float lastTick;
        private float timeStarted;
        private UnitHealth targetHealth;
        public bool Expired => (GameTime.time - timeStarted > duration);

        public DamageOverTimeStatus(Damage damage, float duration)
        {
            this.damage = damage;
            this.duration = duration;
        }


        public void EffectApplied(EffectController target)
        {
            timeStarted = GameTime.time;
            targetHealth = target.GetComponent<UnitHealth>();
        }

        public void Tick(EffectController target)
        {
            if ((Time.time - lastTick) >= TickFrequency)
            {
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }
                lastTick = Time.time;
            }
        }
    }
}