using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float Duration = 1f;
    public float Interval = 0.5f;
    public bool stacksInDuration = false;
    protected abstract void Apply(Unit unit);
    internal virtual void ApplyEffect(string attackerName, ITakeDamage target)
    {
        if(target == null)
            return;
        target.AddEffect(attackerName, new Effect(attackerName, Duration, Interval, Apply), stacksInDuration);
    }
}
