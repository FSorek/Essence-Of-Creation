using System;
using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Game.Entity.Targeting;
using DataBehaviors.Player.States;
using Monobehaviors.BuildSpot;
using Monobehaviors.Game.Managers;
using UnityEngine;

namespace Monobehaviors.Player
{
    [RequireComponent(typeof(PlayerComponent))]
    public class PlayerBuildManager : MonoBehaviour
    {
        public GameObject AirObeliskPrefab;
        private GameObject[] buildBlocks;

        private int buildingBlocksNumber;

        public GameObject BuildSpotBasePrefab;
        public GameObject BuildSpotBlockPrefab;
        public GameObject BuildSpotPeakPrefab;
        public GameObject EarthObeliskPrefab;

        public GameObject FireObeliskPrefab;
        private Vector3 latestBuildSpotDirection;
        private BuildSpotComponent spotComponentOnFocus;
        public GameObject WaterObeliskPrefab;
        private Transform handTransform;
        private bool isBuilding;
        private PlayerComponent player;
        public static event Action<GameObject, BuildSpotComponent> OnTowerCreated = delegate { };


        private void Awake()
        {
            player = GetComponent<PlayerComponent>();
            handTransform = player.HandTransform;
            
            buildingBlocksNumber = 0;
            buildBlocks = new GameObject[3];
            
            AttunedPlayerState.OnElementBuildingStarted += AttunedPlayerStateOnElementBuildingStarted;
            AttunedPlayerState.OnElementBuildingFinished += BuildFinished;

            PlacingBuildSpotPlayerState.OnIncreaseBuildSpotHeight += IncreaseBuildingBlocks;
            PlacingBuildSpotPlayerState.OnDecreaseBuildSpotHeight += ReduceBuildingBlocks;
            PlacingBuildSpotPlayerState.OnBuildSpotCreated += BuildSpotCreated;
            PlacingBuildSpotPlayerState.OnBuildSpotCancelled += BuildSpotCancelled;

        }

        private void AttunedPlayerStateOnElementBuildingStarted(PlayerComponent player, BuildSpotComponent spot)
        {
            spotComponentOnFocus = spot;
        }

        private void BuildSpotCreated()
        {
            var peak = Instantiate(BuildSpotPeakPrefab);
            peak.transform.position =
                buildBlocks[buildingBlocksNumber - 1].transform.position + latestBuildSpotDirection * 2;
            peak.AddComponent<BuildSpotComponent>();
            BuildSpotComponent.BuildSpots.Add(peak.transform);
            for (int i = 0; i < buildingBlocksNumber; i++) buildBlocks[i] = null;
            buildingBlocksNumber = 0;
        }

        private void BuildSpotCancelled()
        {
            for (int i = 0; i < buildingBlocksNumber; i++)
            {
                Destroy(buildBlocks[i].gameObject);
                buildBlocks[i] = null;
            }

            buildingBlocksNumber = 0;
        }

        private void BuildFinished(IPlayer playerC)
        {
            GameObject obelisk = null;
            switch (playerC.CurrentElement)
            {
                case Elements.Fire:
                    obelisk = FireObeliskPrefab;
                    break;
                case Elements.Earth:
                    obelisk = EarthObeliskPrefab;
                    break;
                case Elements.Water:
                    obelisk = WaterObeliskPrefab;
                    break;
                case Elements.Air:
                    obelisk = AirObeliskPrefab;
                    break;
            }

            if (obelisk != null && spotComponentOnFocus != null)
            {
                var tower = Instantiate(obelisk, spotComponentOnFocus.transform.position, Quaternion.identity);
                OnTowerCreated(tower, spotComponentOnFocus);
            }

            isBuilding = false;
        }

        private void Update()
        {
            if(player.CurrentElement != Elements.Invocation) return;
            if (buildingBlocksNumber <= 0)
                IncreaseBuildingBlocks();
            var position = handTransform.position;
            Vector3 closestPoint =
                ClosestEntityFinder.GetClosestTransform(FloorColliderManager.Instance.BuildColliders,
                    position).GetComponent<Collider>().ClosestPointOnBounds(position);
            latestBuildSpotDirection = (position - closestPoint).normalized;

            for (int i = 0; i < buildingBlocksNumber; i++)
            {
                buildBlocks[i].transform.LookAt(position);
                buildBlocks[i].transform.position = closestPoint + i * 3 * latestBuildSpotDirection;
            }
        }

        private void ReduceBuildingBlocks()
        {
            if (buildingBlocksNumber > 1)
            {
                if (buildBlocks[buildingBlocksNumber - 1] != null)
                {
                    Destroy(buildBlocks[buildingBlocksNumber - 1].gameObject);
                    buildBlocks[buildingBlocksNumber - 1] = null;
                }

                buildingBlocksNumber--;
            }
        }

        private void IncreaseBuildingBlocks()
        {
            if (buildingBlocksNumber < 3)
            {
                buildingBlocksNumber++;
                if (buildBlocks[buildingBlocksNumber - 1] == null)
                {
                    if (buildingBlocksNumber == 1)
                    {
                        var block = Instantiate(BuildSpotBasePrefab);
                        buildBlocks[buildingBlocksNumber - 1] = block;
                    }
                    else
                    {
                        var block = Instantiate(BuildSpotBlockPrefab);
                        var scale = block.transform.lossyScale.x * Mathf.Pow(.8f, buildingBlocksNumber);
                        block.transform.localScale = new Vector3(scale,scale, scale);
                        buildBlocks[buildingBlocksNumber - 1] = block;
                    }

                }
            }
        }
    }
}