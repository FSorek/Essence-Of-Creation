using System;
using Data.Game;
using Data.ScriptableObjects.Game;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class BurnHitAbility : HitAbility
    {
        [SerializeField] private DamageData burnDamage;
        [SerializeField] private float duration = 2f;

        private Damage damage;

        private void Awake()
        {
            damage = new Damage(burnDamage);
        }

        public override void ApplyAbility(Transform target)
        {
            target.GetComponent<EffectController>().ApplyStatus(new DamageOverTimeStatus(damage, duration));
        }
    }
}