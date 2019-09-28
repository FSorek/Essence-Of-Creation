using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Duration { get; set; }
    public float Interval { get; set; }
    public bool StacksInDuration { get; set; }
    public abstract void Apply(ITakeDamage unit, int attackerID);
    public abstract void Tick(ITakeDamage unit, int attackerID);
    public abstract void Remove(ITakeDamage unit, int attackerID);
}
