using Monobehaviors.Tower.Attack;

namespace Monobehaviors.Projectiles
{
    public class ProjectileSimpleDeath : IProjectileDeathBehaviour
    {
        public bool CanDestroy()
        {
            return true;
        }
    }
}