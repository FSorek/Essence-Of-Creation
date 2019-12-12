using System.Collections.Generic;
using Monobehaviors.Player;
using UnityEngine;

namespace Monobehaviors.BuildSpot
{
    public class BuildSpotComponent : MonoBehaviour
    {
        public static List<Transform> BuildSpots;
        public bool IsOccupied => CurrentTower != null;
        public GameObject CurrentTower { get; private set; }

        private void Awake()
        {
            if (BuildSpots == null)
                BuildSpots = new List<Transform>();
            BuildSpots.Add(this.transform);
            PlayerBuildManager.OnTowerCreated += AssignTower;
        }

        private void AssignTower(GameObject tower, BuildSpotComponent spotComponent)
        {
            if (spotComponent.transform != transform)
                return;

            CurrentTower = tower;
            PlayerBuildManager.OnTowerCreated -= AssignTower;
        }

        public void SetCurrentTower(GameObject tower)
        {
            CurrentTower = tower;
            PlayerBuildManager.OnTowerCreated -= AssignTower;
        }

        public void ClearCurrentTower()
        {
            CurrentTower = null;
            PlayerBuildManager.OnTowerCreated += AssignTower;
        }
    }
}