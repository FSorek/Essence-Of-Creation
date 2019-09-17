using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BounceProjectile : Projectile
{
    private int availableBounces = -1;
    private Stack<ITakeDamage> previousTargets = new Stack<ITakeDamage>();
    protected override bool TargetHit()
    {
        var bounceAttack = owner as BounceAttackModule;
        if (bounceAttack == null)
        {
            Debug.LogWarning("Projectile spawned without a module.");
            return true;
        }

        if (availableBounces < 0)
            availableBounces = bounceAttack.Bounces;
        if(target != null)
        {
            previousTargets.Push(target);
            target.TakeDamage(owner.AttackerID, owner.AttackData.Damage, owner.ActiveAbilities);
        }
        if (availableBounces <= 0)
            return true;
        availableBounces--;
        var enemies = WaveManager.Instance.GetEnemiesAlive().Except(previousTargets);
        target = RangeTargetScanner<ITakeDamage>.GetTargets(transform.position, enemies.ToArray(), bounceAttack.Range).FirstOrDefault();
        return false;
    }
}