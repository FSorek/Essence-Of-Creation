using UnityEngine;

public class AreaAttackModule : AttackModule
{
    public float Range = 5f;

    public override void Attack(ITakeDamage target)
    {
        var projectile = GameObject.Instantiate(AttackData.projectileModel, Obelisk.AttackSpawnPosition, Quaternion.identity);
        projectile.AddComponent<AreaProjectile>().Initialize(target, this);
    }
}
