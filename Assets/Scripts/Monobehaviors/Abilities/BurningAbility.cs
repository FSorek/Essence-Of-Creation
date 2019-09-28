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

    public override void Remove(ITakeDamage unit, int attackerID)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick(ITakeDamage unit, int attackerID)
    {
        throw new System.NotImplementedException();
    }
}
