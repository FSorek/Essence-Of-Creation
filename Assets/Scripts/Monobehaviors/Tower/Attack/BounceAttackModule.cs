using UnityEngine;

public class BounceAttackModule : AttackModule
{
    public float Range = 5f;
    public int Bounces = 2;

    public override void Attack(ITakeDamage target)
    {
        var projectile = GameObject.Instantiate(AttackData.projectileModel, Entity.Position, Quaternion.identity);
        projectile.AddComponent<BounceProjectile>().Initialize(target, this);
    }
}
