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
        }

        public void ClearCurrentEssence()
        {
            CurrentEssence = null;
        }
    }
}