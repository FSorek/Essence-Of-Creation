using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Tower.Attack
{
    public abstract class AttackProjectileModifier : ScriptableObject
    {
        public abstract void ApplyModification(Projectile projectile);
    }
}