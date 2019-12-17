using System.Collections;
using Data.Interfaces.Pooling;
using UnityEngine;

namespace Data.Extensions
{
    public static class GameObjectPoolExtension
    {
        public static void ReturnToPool(this GameObject obj)
        {
            obj.GetComponent<IGameObjectPooled>()?.Pool.ReturnToPool(obj);
        }

        public static IEnumerator DelayedReturnToPool(this GameObject obj, float delay)
        {
            yield return obj.GetComponent<IGameObjectPooled>()?.Pool.DelayedReturnToPool(obj, delay);
        }
    }
}