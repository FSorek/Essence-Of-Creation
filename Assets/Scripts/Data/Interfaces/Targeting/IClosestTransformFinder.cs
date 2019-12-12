using UnityEngine;

namespace Data.Interfaces.Targeting
{
    public interface IClosestTransformFinder
    {
        Transform GetClosestTransform(Transform[] nearbyBuildSpots, Vector3 currentPosition);
    }
}