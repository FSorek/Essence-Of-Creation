using System;
using UnityEngine;

public static class ClosestEntityFinder<T> where T : IEntity
{
    public static T GetClosestTransform(T[] nearbyTransforms, Vector3 currentPosition)
    {
        if (nearbyTransforms == null || nearbyTransforms.Equals(null) || currentPosition == null)
            return default(T);
        if (nearbyTransforms.Length > 0)
        {
            var closestDist = Mathf.Infinity;
            T closest = default(T);
            for (int i = 0; i < nearbyTransforms.Length; i++)
            {
                var currentDistance = Vector3.Distance(currentPosition, nearbyTransforms[i].Position);
                if (currentDistance < closestDist)
                {
                    closestDist = currentDistance;
                    closest = nearbyTransforms[i];
                }
            }

            return closest;
        }
        return default(T);
    }
}
