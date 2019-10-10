using System;
using UnityEngine;

public class ProjectileController
{
    protected Vector3 targetKnownPos;
    protected ITakeDamage target;
    protected ICanAttack owner;
    protected IEntity projectile;

    protected ITowerLastingAbility[] ActiveTowerAbilities;
    protected EntitySimpleMove movement;

    public ProjectileController(ITakeDamage target, ICanAttack owner, IEntity projectile)
    {
        this.owner = owner;
        this.target = target;
        this.projectile = projectile;
        targetKnownPos = target == null ? Vector3.zero : target.Position;

        movement = new EntitySimpleMove(projectile);
    }

    public void Tick(Func<bool> TargetHit)
    {
        if(target == null || target.Equals(null))
        {
            projectile.Destroy();
            return;
        }
        var direction = target.Position - projectile.Position;
        var distanceThisFrame = GameTime.deltaTime * owner.AttackData.ProjectileSpeed;
        if (direction.magnitude <= distanceThisFrame)
        {
            if (TargetHit())
                projectile.Destroy();
        }
        if (owner.AttackData.CanFollowTarget)
        {
            targetKnownPos = target.Position;
            movement.Move(target.Position, owner.AttackData.ProjectileSpeed);
        }
        else
        {
            movement.Move(targetKnownPos, owner.AttackData.ProjectileSpeed);
        }
    }
}
