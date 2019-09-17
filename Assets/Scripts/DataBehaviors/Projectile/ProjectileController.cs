using UnityEngine;

public class ProjectileController
{
    protected Vector3 targetKnownPos;
    protected ITakeDamage target;
    protected ICanAttack owner;
    protected IEntity projectile;

    protected Ability[] activeAbilities;
    protected IEntitySimpleMove movement;

    public ProjectileController(ITakeDamage target, ICanAttack owner, IEntity projectile)
    {
        this.owner = owner;
        this.target = target;
        this.projectile = projectile;
        targetKnownPos = target.Position;

        movement = new EntitySimpleMove(projectile);
    }

    public void Tick(bool targetHit)
    {
        var direction = target.Position - projectile.Position;
        var distanceThisFrame = Time.deltaTime * owner.AttackData.ProjectileSpeed;
        if (direction.magnitude <= distanceThisFrame)
        {
            if (targetHit)
                projectile.Destroy();
        }
        if (owner.AttackData.CanFollowTarget && target != null)
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
