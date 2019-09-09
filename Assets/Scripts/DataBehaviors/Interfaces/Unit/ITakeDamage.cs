using System.Collections;
using System.Collections.Generic;

public interface ITakeDamage : IEntity
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(Damage damage);
    void OnDeath();

    void AddEffect(string attackerName, Effect effect, bool stacksInDuration);
}
