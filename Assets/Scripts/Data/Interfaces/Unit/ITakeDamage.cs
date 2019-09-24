using System;
using System.Collections;
using System.Collections.Generic;

public interface ITakeDamage : IEntity
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    ArmorType ArmorType { get; }

    void TakeDamage(int attackerID, Damage damage, Ability[] abilities = null);
    event Action<Damage> OnTakeDamage;
    void RemoveEffect(Effect effect);
}
