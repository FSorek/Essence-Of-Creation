using DataBehaviors.Game.Utility;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public class SimpleMove : IMover
    {
        private Transform owner;
        public SimpleMove(Transform owner)
        {
            this.owner = owner;
        }
        
        public void Move(Vector3 position, float speed)
        {
            owner.transform.position += speed * GameTime.deltaTime * (position - owner.transform.position).normalized;
        }
    }
}
