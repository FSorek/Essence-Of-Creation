using UnityEngine;

public interface IClosestTransformFinder
{
    Transform GetClosestTransform(Transform[] nearbyBuildSpots, Vector3 currentPosition);
}