using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class FloorColliderManager : MonoBehaviour
    {
        public static FloorColliderManager Instance;
        [SerializeField] private Transform floorColliderParent;
        private Transform[] buildAreaTransforms = new Transform[0];

        public Transform[] BuildAreaTransforms => buildAreaTransforms;

        public void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;

            buildAreaTransforms = GetFloorColliders();
        }

        private Transform[] GetFloorColliders()
        {
            int children = floorColliderParent.childCount;
            var colliders = new Transform[children];
            for (int i = 0; i < children; ++i)
                colliders[i] = floorColliderParent.GetChild(i);
            return colliders;
        }
    }
}