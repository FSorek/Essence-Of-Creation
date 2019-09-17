public class AreaProjectile : Projectile
{
    protected override bool TargetHit()
    {
        var aoeAttack = owner as AreaAttackModule;
        if (aoeAttack == null)
            return true;
        var targets = RangeTargetScanner<ITakeDamage>.GetTargets(transform.position, WaveManager.Instance.GetEnemiesAlive(), aoeAttack.Range);
        for(int i = 0; i<targets.Length;i++)
        {
            targets[i].TakeDamage(owner.AttackerID, owner.AttackData.Damage, owner.ActiveAbilities);
        }
        return true;
    }
}
