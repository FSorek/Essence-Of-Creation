using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RangeTargetScanner<T> where T : IEntity
{
    public static T[] GetTargets(Vector3 currentPosition, IEntity[] enemies, float range)
    {
        List<T> targets = new List<T>();

        foreach (T potentialTarget in enemies)
        {
            var dx = Mathf.Abs(potentialTarget.Position.x - currentPosition.x);
            if (dx > range)
                continue;
            var dy = Mathf.Abs(potentialTarget.Position.z - currentPosition.z);
            if(dy > range)
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
