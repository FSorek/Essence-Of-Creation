using UnityEngine;
using UnityEngine.AI;

namespace Monobehaviors.Projectiles
{
    public class NavMeshMove : IMover
    {
        private Vector3 lastTargetPosition;
        private readonly NavMeshAgent agent;
        public NavMeshMove(Transform owner)
        {
            agent = owner.GetComponent<NavMeshAgent>();
            if(agent == null) return;
            agent.enabled = true;

        }
        public void Move(Vector3 position, float speed)
        {
            agent.speed = speed;
            if(agent== null || lastTargetPosition == position) return;

            agent.SetDestination(position);
        }
    }
}