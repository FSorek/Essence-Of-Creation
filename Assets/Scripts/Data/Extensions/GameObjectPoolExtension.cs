﻿using Data.Interfaces.Pooling;
using UnityEngine;

namespace Data.Extensions
{
    public static class GameObjectPoolExtension
    {
        public static void ReturnToPool(this GameObject obj, float delay = 0f)
        {
            obj.GetComponent<IGameObjectPooled>()?.Pool.ReturnToPool(obj, delay);
        }
    }
}