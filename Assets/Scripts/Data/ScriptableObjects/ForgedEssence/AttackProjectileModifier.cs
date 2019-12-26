using Monobehaviors.Projectiles;
using UnityEngine;

namespace Data.ScriptableObjects.ForgedEssence
{
    public abstract class AttackProjectileModifier : ScriptableObject
    {
        public abstract void ApplyModification(Projectile projectile);
    }
}