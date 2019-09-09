/// <summary>
/// A simple DoT ability, low damage, stacks intensity and only works on non-shielded units
/// </summary>

public class BleedAbility : Ability
{
    public Damage damage;
    protected override void Apply(Unit unit)
    {
        // if(unit.Shield <= 0)
        unit.TakeDamage(damage);
    }
}

