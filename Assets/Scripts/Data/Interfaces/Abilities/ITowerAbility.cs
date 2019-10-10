using UnityEngine;

public interface ITowerAbility
{
    bool Stacks { get; }
    void Apply(IUnit unit, int attackerID);
    void Tick(IUnit unit, int attackerID);
    void Remove(IUnit unit, int attackerID);
}
