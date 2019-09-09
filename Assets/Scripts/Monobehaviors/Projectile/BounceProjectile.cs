using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BounceProjectile : Projectile
{
    private int availableBounces = -1;
    private Stack<ITakeDamage> previousTargets = new Stack<ITakeDamage>();
    protected override bool OnTargetHit()
    {
        var bounceAttack = Module as BounceAttackModule;
        if (bounceAttack == null)
            return true;

        if (availableBounces < 0)
            availableBounces = bounceAttack.Bounces;
        if(target != null)
        {
            previousTargets.Push(target);
            target.TakeDamage(attackData.Damage);
            ApplyActiveEffects(target);
        }
        if (availableBounces <= 0)
            return true;
        availableBounces--;
        var enemies = WaveManager.Instance.GetEnemiesAlive().Except(previousTargets);
        target = RangeTargetScanner<ITakeDamage>.GetTargets(transform.position, enemies.ToArray(), bounceAttack.Range).FirstOrDefault();
        return false;
    }
}