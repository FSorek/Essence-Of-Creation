using System;
using Data.Data_Types;
using DataBehaviors.Game.Systems;
using UnityEngine;

namespace Monobehaviors.Unit
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
            maxHealth = statController.GetStat(StatName.HealthPool);
            if(maxHealth == null)
                return;
            currentHealth = new Stat(maxHealth);
        }

        public void TakeDamage(Damage damage)
        {
            currentHealth -= damage.GetDamageToArmor(armorType);
            OnTakeDamage(damage);
        }
    }
}