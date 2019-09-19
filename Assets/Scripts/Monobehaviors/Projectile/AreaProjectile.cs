using UnityEngine;

public class AreaProjectile : Projectile
{
    protected override bool TargetHit()
    {
        var aoeAttack = owner as AreaAttackModule;
        if (aoeAttack == null)
        {
            Debug.LogWarning("Area projectile without a module.");
            return true;
        }
        var targets = RangeTargetScanner<ITakeDamage>.GetTargets(transform.position, WaveManager.Instance.GetEnemiesAlive(), aoeAttack.Range);
        for(int i = 0; i<targets.Length;i++)
        {
            targets[i].TakeDamage(owner.AttackerID, owner.AttackData.Damage, owner.ActiveAbilities);
        }
        return true;
    }
}
