using System.Collections;
using System.Collections.Generic;

public interface ITakeDamage : IEntity
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(int attackerID, Damage damage, Ability[] abilities = null);
    void OnDeath();

    void AddEffect(int attackerID, Effect effect);
    void RemoveEffect(Effect effect);
}
