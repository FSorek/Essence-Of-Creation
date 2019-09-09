using System;
using UnityEngine;

public class SingleProjectile : Projectile
{
    protected override bool OnTargetHit()
    {
        if(target == null)
            return true;
        else
        {
            target.TakeDamage(attackData.Damage);
            ApplyActiveEffects(target);
            return true;
        }
    }
}