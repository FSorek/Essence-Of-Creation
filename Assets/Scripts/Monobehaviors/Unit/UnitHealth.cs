using Data.Data_Types;
using DataBehaviors.Game.Systems;
using UnityEngine;

namespace Monobehaviors.Unit
{
    [RequireComponent(typeof(UnitComponent))]
    public class UnitHealth : MonoBehaviour
    {
        public float CurrentHealth => currentHealth;

        private ArmorType armorType;
        [SerializeField] private float maxHealth;
        private UnitComponent unit;
        private float currentHealth;
        private void Awake()
        {
            unit = GetComponent<UnitComponent>();
            unit.OnTakeDamage += UnitOnTakeDamage;
            unit.RegisterStat(StatName.MaxHealth, maxHealth);
            currentHealth = unit.GetStat(StatName.MaxHealth);
        }
        private void UnitOnTakeDamage(Damage damage)
        {
            currentHealth -= damage.GetDamageToArmor(armorType);

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}