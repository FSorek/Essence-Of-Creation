using System.Collections.Generic;
using System.Linq;

public class UnitController
{
    private ITakeDamage owner;
    private float currentHealth;
    private List<Effect> activeEffects;

    public UnitController(ITakeDamage owner)
    {
        this.owner = owner;
        currentHealth = owner.MaxHealth;
        activeEffects = new List<Effect>();
    }

    public void TakeDamage(int attackerID, Damage damage, Ability[] abilities = null)
    {
        var finalDamage = damage.GetDamageToArmor(owner.ArmorType);
        currentHealth -= finalDamage;
        if (currentHealth <= 0)
        {
            owner.Destroy();
            return;
        }
        if (abilities != null)
        {
            for (int i = 0; i < abilities.Length; i++)
                AddEffect(attackerID, new Effect(attackerID, abilities[i]));
        }
    }

    public void AddEffect(int attackerID, Effect effect)
    {
        var currentInstance = activeEffects.FirstOrDefault(a => a.AttackerId == attackerID);
        if (effect.StackInDuration && currentInstance != null)
        {
            currentInstance.Extend();
        }
        else
        {
            activeEffects.Add(effect);
        }
    }
    public void RemoveEffect(Effect effect) => activeEffects.Remove(effect);

    public float CurrentHealth => currentHealth;
}
