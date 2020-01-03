using System;
using System.Collections.Generic;
using Data.Data_Types;
using Data.Extensions;
using Data.Game;
using Data.ScriptableObjects.Game;
using Monobehaviors.Essences.Attacks;
using Monobehaviors.Game;
using UnityEngine;

namespace Monobehaviors.Units
{
    [RequireComponent(typeof(StatController))]
    public class UnitHealth : MonoBehaviour
    {
        public event Action<Damage> OnTakeDamage = delegate {  };

        [SerializeField]private ArmorType armorType;
        private Stat currentHealth;
        private Stat maxHealth;
        private StatController statController;
        public Stat CurrentHealth => currentHealth;
        public Stat MaxHealth => maxHealth;

        private void Awake()
        {
            statController = GetComponent<StatController>();
        }

        private void OnEnable()
        {
            maxHealth = statController.GetStat(StatName.HealthPool);
            if(maxHealth == null)
                return;
            currentHealth = new Stat(maxHealth);
        }

        public void TakeDamage(Damage damage)
        {
            foreach (var damagePair in damage.Damages)
            {
                var finalDamage = damagePair.Value.ModValue;
                if (damagePair.Key.ArmorMultipliers.ContainsKey(armorType))
                    finalDamage *= damagePair.Key.ArmorMultipliers[armorType];

                currentHealth -= finalDamage;
            }
            
            OnTakeDamage(damage);
            if(currentHealth <= 0)
                gameObject.ReturnToPool();
        }
    }
}