using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Duration { get; set; }
    public float Interval { get; set; }
    public bool StacksInDuration { get; set; }
    public abstract void Apply(ITakeDamage unit, int attackerID);
}
