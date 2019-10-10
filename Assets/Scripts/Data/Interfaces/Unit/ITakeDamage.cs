using System;
using System.Collections;
using System.Collections.Generic;

public interface ITakeDamage : IEntity
{
    Stat MaxHealth { get; }
    Stat CurrentHealth { get; }
    Stat HealthRegeneration { get; }
    Stat ArmorLayers { get; }
    Stat CrystallineLayers { get; }
    ArmorType ArmorType { get; }

    void TakeDamage(int attackerID, Damage damage);
    event Action<Damage> OnTakeDamage;
}
