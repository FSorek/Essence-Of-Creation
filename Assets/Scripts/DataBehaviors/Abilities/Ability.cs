using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Duration = 1f;
    public float Interval = 0.5f;
    public bool stacksInDuration = false;
    public abstract void Apply(ITakeDamage unit, int attackerID);}
