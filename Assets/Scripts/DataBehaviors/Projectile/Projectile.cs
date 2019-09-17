using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public abstract class Projectile : GameEntity
{
    protected ProjectileController projectileController;
    protected abstract bool TargetHit();

    protected ICanAttack owner;
    protected ITakeDamage target;

    private bool initialized = false;

    public void Initialize(ITakeDamage target, ICanAttack owner)
    {
        this.target = target;
        this.owner = owner;
        projectileController = new ProjectileController(target, owner, this);
        initialized = true;
    }

    private void Update()
    {
        if(!initialized)
            return;

        projectileController.Tick(TargetHit());
    }
}
