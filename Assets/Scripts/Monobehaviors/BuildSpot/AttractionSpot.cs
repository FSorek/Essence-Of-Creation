using System.Collections.Generic;
using Monobehaviors.Player;
using UnityEngine;

namespace Monobehaviors.BuildSpot
{
    public class AttractionSpot : MonoBehaviour
    {
        public static readonly List<Transform> AttractionSpots = new List<Transform>();
        public bool IsOccupied => CurrentEssence != null;
        public GameObject CurrentEssence { get; private set; }

        private void Awake()
        {
            AttractionSpots.Add(transform);
        }

        public void AssignEssence(GameObject tower)
        {
            CurrentEssence = tower;
        }

        public void ClearCurrentEssence()
        {
            CurrentEssence = null;
        }
    }
}