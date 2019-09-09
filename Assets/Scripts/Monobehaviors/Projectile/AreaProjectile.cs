public class AreaProjectile : Projectile
{
    protected override bool OnTargetHit()
    {
        var aoeAttack = Module as AreaAttackModule;
        if (aoeAttack == null)
            return true;
        var targets = RangeTargetScanner<ITakeDamage>.GetTargets(transform.position, WaveManager.Instance.GetEnemiesAlive(), aoeAttack.Range);
        for(int i = 0; i<targets.Length;i++)
        {
            ApplyActiveEffects(targets[i]);
            targets[i].TakeDamage(attackData.Damage);
        }
        return true;
    }
}
