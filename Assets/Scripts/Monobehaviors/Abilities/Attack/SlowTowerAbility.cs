using UnityEngine;

public class SlowTowerAbility : MonoBehaviour, ITowerLastingAbility
{
    public Damage damage;
    [SerializeField] private float duration = 2;
    [SerializeField] private float interval = 1;
    [SerializeField] private bool stacksInDuration = false;
    StatModifier slow = new StatModifier(-30, StatModifierType.Percent);

    public void Apply(IUnit unit, int attackerID)
    {
        unit.MovementSpeed.AddModifier(slow);
    }

    public void Remove(IUnit unit, int attackerID)
    {
        unit.MovementSpeed.RemoveModifier(slow);
    }

    public void Tick(IUnit unit, int attackerID)
    {
        slow.Value -= 5;
        
        unit.MovementSpeed.SetDirty();
    }

    public float Duration => duration;
    public float Interval => interval;
    public bool Stacks => stacksInDuration;
}