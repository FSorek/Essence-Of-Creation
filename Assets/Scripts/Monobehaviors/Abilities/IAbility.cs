using UnityEngine;

public interface IAbility
{
    float Duration { get; }
    float Interval { get; }
    bool StacksInDuration { get; }
    void Apply(IUnit unit, int attackerID);
    void Tick(IUnit unit, int attackerID);
    void Remove(IUnit unit, int attackerID);
}
