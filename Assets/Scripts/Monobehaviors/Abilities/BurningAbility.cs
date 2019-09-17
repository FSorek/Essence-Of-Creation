/// <summary>
/// A simple DoT ability, high damage, stacks duration
/// </summary>
public class BurningAbility : Ability
{
    public Damage Damage;
    public override void Apply(ITakeDamage unit, int attackerID)
    {
        unit.TakeDamage(attackerID, Damage);
    }
}
