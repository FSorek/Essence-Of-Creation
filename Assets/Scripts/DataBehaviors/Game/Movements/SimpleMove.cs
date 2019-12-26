using Data.Interfaces.Game;
using DataBehaviors.Game.Utility;
using UnityEngine;

namespace DataBehaviors.Game.Movements
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
