using UnityEngine;

namespace DataBehaviors.Game.Targeting
{
    public static class ClosestEntityFinder
    {
        public static Transform GetClosestTransform(Transform[] nearbyTransforms, Vector3 currentPosition)
        {
            if (nearbyTransforms == null || nearbyTransforms.Equals(null))
                return default;
            if (nearbyTransforms.Length > 0)
            {
                float closestDist = Mathf.Infinity;
                Transform closest = null;
                for (int i = 0; i < nearbyTransforms.Length; i++)
                {
                    float currentDistance = Vector3.Distance(currentPosition, nearbyTransforms[i].transform.position);
                    if (currentDistance < closestDist)
                    {
                        closestDist = currentDistance;
                        closest = nearbyTransforms[i];
                    }
                }

                return closest;
            }

            return null;
        }
    }
}