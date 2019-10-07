using UnityEngine;

public class AttackController
{
    private float lastRecordedAttackTime = 0;
    private ICanAttack owner;
    private IWaveManager waveManager;
    private ITakeDamage[] targets;

    public AttackController(ICanAttack owner, IWaveManager waveManager)
    {
        this.owner = owner;
        this.waveManager = waveManager;
        targets = new ITakeDamage[owner.AttackData.TargetLimit];
    }

    public void Tick()
    {
        if (CanAttack())
        {
            DoAttack();
        }
    }

    public bool CanAttack()
    {
        if (GameTime.time - lastRecordedAttackTime >= owner.AttackData.AttackTimer)
        {
            targets = GetTargets();
            if (targets == null || targets.Length <= 0)
                return false;
            lastRecordedAttackTime = GameTime.time;
            return true;
        }
        return false;
    }

    private void DoAttack()
    {
        for (int i = 0; i < owner.AttackData.TargetLimit; i++)
        {
            if (targets[i] != null)
            {
                owner.Attack(targets[i]);
            }
        }
    }

    protected ITakeDamage[] GetTargets()
    {
        var enemies = waveManager.EnemiesAlive;
        if (enemies.Length <= 0)
            return null;
        var targets = new ITakeDamage[owner.AttackData.TargetLimit];
        var enemiesInRange = RangeTargetScanner<ITakeDamage>.GetTargets(owner.Entity.Position, enemies, owner.AttackData.Range);

        if (enemiesInRange.Length > 0)
        {
            for (int i = 0; i < Mathf.Min(owner.AttackData.TargetLimit, enemiesInRange.Length); i++)
            {
                targets[i] = enemiesInRange[i];
            }
        }
        else
        {
            for (int i = 0; i < owner.AttackData.TargetLimit; i++)
            {
                targets[i] = null;
            }
        }

        return targets;
    }
}