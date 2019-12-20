using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public interface IMover
    {
        void Move(Vector3 position, float speed);
    }
}