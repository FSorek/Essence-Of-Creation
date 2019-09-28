/// <summary>
/// A simple DoT ability, low damage, stacks intensity and only works on non-shielded units
/// </summary>

public class BleedAbility : Ability
{
    public Damage damage;

    public override void Apply(ITakeDamage unit, int attackerID)
    {
        // if(unit.Shield <= 0)
        unit.TakeDamage(attackerID, damage);
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

