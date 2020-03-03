using Data.Interfaces.Projectiles;

namespace DataBehaviors.Projectiles
{
    public class ProjectileSimpleDeath : IProjectileDeathBehaviour
    {
        public bool CanDestroy()
        {
            return true;
        }
    }
}