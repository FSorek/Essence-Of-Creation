using System;
using UnityEngine;

public class SingleProjectile : Projectile
{
    protected override bool TargetHit()
    {
        if(target == null)
            return true;
        else
        {
            target.TakeDamage(owner.AttackerID, owner.AttackData.Damage, owner.ActiveAbilities);
            return true;
        }
    }
}