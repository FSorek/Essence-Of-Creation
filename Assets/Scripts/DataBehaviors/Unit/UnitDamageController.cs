using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitDamageController
{
    private IUnit owner;
    private float currentHealth;
    private float lastRegenTime;
    private int lastHealthStage;
    private Damage totalDamageTaken;

    public UnitDamageController(IUnit owner)
    {
        this.owner = owner;
        currentHealth = owner.MaxHealth;
        lastRegenTime = GameTime.time;
        lastHealthStage = 4;
    }

    public void TakeDamage(int attackerID, Damage incomingDamage)
    {
        totalDamageTaken += incomingDamage;
        var damage = new Damage();
        damage += incomingDamage;

        var dominatingElement = totalDamageTaken.GetDominatingDamage();
        SetElementalArmorReduction(damage, dominatingElement);
        Crystalize(damage, dominatingElement);
        var finalDamage = damage.GetDamageToArmor(owner.ArmorType);
        
        finalDamage -= owner.ArmorLayers;
        if (finalDamage <= 0)
            finalDamage = 0;
        currentHealth -= finalDamage;
        if (currentHealth <= 0)
        {
            owner.Destroy();
            return;
        }
    }

    private void Crystalize(Damage damage, Elements dominatingElement)
    {
        if (owner.CrystallineLayers > 0)
        {
            var currentHealthStage = Mathf.CeilToInt(HealthPercentage / 25f);
            var strength = 1 - (owner.CrystallineLayers * (4 - currentHealthStage) * .02f);
            for (int i = 4; i > currentHealthStage; i--)
            {
                
            }
            switch (dominatingElement)
            {
                case Elements.Fire:
                    damage.Fire.BaseValue *= strength;
                    break;
                case Elements.Earth:
                    damage.Earth.BaseValue *= strength;
                    break;
                case Elements.Water:
                    damage.Water.BaseValue *= strength;
                    break;
                case Elements.Air:
                    damage.Air.BaseValue *= strength;
                    break;
            }
        }
    }

    private void SetElementalArmorReduction(Damage damage, Elements dominatingElement)
    {
        if(owner.ArmorType != ArmorType.Elemental)
            return;
        switch (dominatingElement)
        {
            case Elements.Fire:
                damage.Fire.BaseValue *= .5f;
                break;
            case Elements.Earth:
                damage.Fire.BaseValue *= .5f;
                break;
            case Elements.Water:
                damage.Fire.BaseValue *= .5f;
                break;
            case Elements.Air:
                damage.Fire.BaseValue *= .5f;
                break;
        }
    }

    public void RegenerateHealth()
    {
        if ((GameTime.time - lastRegenTime) >= 1f && currentHealth < owner.MaxHealth)
        {
            if (currentHealth + owner.HealthRegeneration >= owner.MaxHealth)
                currentHealth = owner.MaxHealth;
            else
                currentHealth += owner.HealthRegeneration;
            lastRegenTime = GameTime.time;
        }
    }

    public float HealthPercentage => (currentHealth / owner.MaxHealth) * 100;
    public float CurrentHealth => currentHealth;
}
