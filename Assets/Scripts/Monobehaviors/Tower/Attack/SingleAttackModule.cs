using UnityEngine;

public class SingleAttackModule : AttackModule
{

    public override void Attack(ITakeDamage target)
    {
        var projectile = GameObject.Instantiate(AttackData.projectileModel, Obelisk.AttackSpawnPosition, Quaternion.identity);
        projectile.AddComponent<SingleProjectile>().Initialize(target, this);
    }
}
