using System.Collections.Generic;
using UnityEngine;

namespace DataBehaviors.Game.Entity.Targeting
{
    public static class RangeTargetScanner
    {
        public static Transform[] GetTargets(Vector3 currentPosition, Transform[] targetPool, float range)
        {
            var targets = new List<Transform>();

            foreach (Transform potentialTarget in targetPool)
            {
                float dx = Mathf.Abs(potentialTarget.transform.position.x - currentPosition.x);
                if (dx > range)
                    continue;
                float dy = Mathf.Abs(potentialTarget.transform.position.z - currentPosition.z);
                if (dy > range)
                    continue;
                if (dx + dy <= range)
                {
                    targets.Add(potentialTarget);
                    continue;
                }

                if (dx * dx + dy * dy <= range * range)
                    targets.Add(potentialTarget);
            }

            return targets.ToArray();
        }
    }
}