/// <summary>
/// A simple DoT ability, high damage, stacks duration
/// </summary>
public class BurningAbility : Ability
{
    public Damage Damage;
    protected override void Apply(Unit unit)
    {
        unit.TakeDamage(Damage);
    }
}
