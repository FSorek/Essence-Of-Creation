using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public abstract class Projectile : MonoBehaviour
{
    private bool Initialized { get; set; }

    protected Vector3 targetKnownPos;
    protected ITakeDamage target;
    protected TowerAttackData attackData;

    protected Ability[] activeAbilities;
    protected AttackModule Module;
    protected string attackerName;

    public void Initialize(ITakeDamage target, AttackModule module)
    {
        this.Module = module;
        this.attackerName = module.AttackData.Name;
        this.attackData = module.AttackData;
        this.activeAbilities = module.GetComponents<Ability>();
        this.target = target;
        targetKnownPos = target.Position;
        Initialized = true;
    }
    protected abstract bool OnTargetHit();

    protected virtual void FixedUpdate()
    {
        if (!Initialized)
            return;
        var distanceThisFrame = Time.deltaTime * attackData.ProjectileSpeed; // this is silly and could be better
        if (Vector3.Distance(transform.position, targetKnownPos) <= distanceThisFrame) // should avoid using V3.Distance 
        {
            if(OnTargetHit())
                OnDeath();
        }
        if (attackData.CanFollowTarget && target != null)
        {
            targetKnownPos = target.Position;
            Move(target.Position);
        }
        else
        {
            Move(targetKnownPos);
        }
    }

    protected void ApplyActiveEffects(ITakeDamage target)
    {
        if(target == null)
            return;
        for (int i = 0; i < activeAbilities.Length; i++)
        {
            activeAbilities[i].ApplyEffect(attackerName, target);
        }
    }

    private void OnDeath()
    {
        GetComponent<VisualEffect>().Stop();
        Destroy(this.gameObject, 4f);
        Destroy(this);
    }

    private void Move(Vector3 position)
    {
        transform.Translate((position - this.transform.position).normalized * Time.deltaTime * attackData.ProjectileSpeed);
    }
}