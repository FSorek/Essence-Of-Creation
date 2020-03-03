using System.Collections.Generic;
using UnityEngine;

namespace Monobehaviors.AttractionSpots
{
    public class AttractionSpot : MonoBehaviour
    {
        public bool IsOccupied => CurrentEssence != null;
        public GameObject CurrentEssence { get; private set; }

        public void AssignEssence(GameObject essence)
        {
            CurrentEssence = essence;
            essence.transform.SetParent(transform);
            essence.transform.position = transform.position;
        }

        public void ClearCurrentEssence()
        {
            CurrentEssence.transform.SetParent(null);
            CurrentEssence = null;
        }
    }
}