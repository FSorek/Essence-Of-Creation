using Data.ScriptableObjects.Globals;
using UnityEngine;

namespace Monobehaviors.Game
{
    public class ManageGlobalList : MonoBehaviour
    {
        public TransformList ListAsset;

        private void OnEnable()
        {
            ListAsset.Add(transform);
        }

        private void OnDisable()
        {
            ListAsset.Remove(transform);
        }
    }
}