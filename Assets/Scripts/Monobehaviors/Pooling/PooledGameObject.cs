using Data.Interfaces.Pooling;
using UnityEngine;

namespace Monobehaviors.Pooling
{
    public class PooledGameObject : MonoBehaviour, IGameObjectPooled
    {
        public ObjectPool Pool { get; set; }
    }
}