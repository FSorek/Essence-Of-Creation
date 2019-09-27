using System.Collections.Generic;
using System.Linq;

public class UnitController
{
    private ITakeDamage owner;
    private float currentHealth;
    private List<Effect> activeEffects;
    private float lastRegenTime;

    public UnitController(ITakeDamage owner)
    {
        this.owner = owner;
        currentHealth = owner.MaxHealth;
        activeEffects = new List<Effect>();
        lastRegenTime = GameTime.time;
    }

    public void TakeDamage(int attackerID, float finalDamage, Ability[] abilities = null)
    {
        currentHealth -= finalDamage;
        if (currentHealth <= 0)
        {
            owner.Destroy();
            return;
        }
        if (abilities != null)
        {
            for (int i = 0; i < abilities.Length; i++)
                AddEffect(new Effect(attackerID, abilities[i]));
        }
    }

    public void AddEffect(Effect effect)
    {
        var currentInstance = activeEffects.FirstOrDefault(a => a.OwnerId == effect.OwnerId && a.EffectTick == effect.EffectTick);
        if (effect.StackInDuration && currentInstance != null)
        {
            currentInstance.Extend();
        }
        else
        {
            activeEffects.Add(effect);
        }
    }

    public void RegenerateHealth()
    {
        if ((GameTime.time - lastRegenTime) > 1f && currentHealth < owner.MaxHealth)
        {
            if (currentHealth + owner.HealthRegeneration >= owner.MaxHealth)
                currentHealth = owner.MaxHealth;
            else
                currentHealth += owner.HealthRegeneration;
            lastRegenTime = GameTime.time;
        }
    }
    public void RemoveEffect(Effect effect) => activeEffects.Remove(effect);

    public float CurrentHealth => currentHealth;
    public List<Effect> ActiveEffects => activeEffects;
}
