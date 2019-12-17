using DataBehaviors.Game.Utility;
using UnityEngine;

namespace Monobehaviors.Projectiles
{
    public class SimpleMove
    {
        private Transform owner;
        public SimpleMove(Transform owner)
        {
            this.owner = owner;
        }
        
        public void Move(Vector3 position, float speed)
        {
            owner.transform.position += (position - owner.transform.position).normalized * GameTime.deltaTime * speed;
        }
    }
}