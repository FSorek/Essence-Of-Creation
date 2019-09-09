using System;
using UnityEngine;

public class PlayerBuildManager : MonoBehaviour
{
    public static event Action<GameObject, BuildSpot> OnTowerCreated = delegate {};

    public GameObject FireObeliskPrefab;
    public GameObject AirObeliskPrefab;
    public GameObject WaterObeliskPrefab;
    public GameObject EarthObeliskPrefab;

    public GameObject BuildSpotBlockPrefab;
    public GameObject BuildSpotPeakPrefab;

    private int buildingBlocksNumber;
    private GameObject[] buildBlocks;
    private Vector3 latestBuildSpotDirection;
    private BuildSpot spotOnFocus;
    

    private void Awake()
    {
        buildingBlocksNumber = 0;
        AttunedPlayerState.OnElementBuildingStarted += (playerC, spot) => spotOnFocus = spot;
        AttunedPlayerState.OnElementBuildingFinished += BuildFinished;

        PlacingBuildSpotPlayerState.OnIncreaseBuildSpotHeight += IncreaseBuildingBlocks;
        PlacingBuildSpotPlayerState.OnDecreaseBuildSpotHeight += ReduceBuildingBlocks;
        PlacingBuildSpotPlayerState.PlacingBuildSpot = PlacingBuildSpot;
        PlacingBuildSpotPlayerState.OnBuildSpotCreated += BuildSpotCreated;
        PlacingBuildSpotPlayerState.OnBuildSpotCancelled += BuildSpotCancelled;

        buildBlocks = new GameObject[3];
    }

    private void BuildSpotCreated()
    {
        var peak = Instantiate(BuildSpotPeakPrefab);
        peak.transform.position = buildBlocks[buildingBlocksNumber - 1].transform.position + latestBuildSpotDirection * 2;
        peak.AddComponent<BuildSpot>();
        BuildSpotManager.BuildSpots.Add(peak.GetComponent<IEntity>());
        for (int i = 0; i < buildingBlocksNumber; i++)
        {
            buildBlocks[i] = null;
        }
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

    private void BuildFinished(PlayerController playerC)
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

        if (obelisk != null && spotOnFocus != null)
        {
            var tower = Instantiate(obelisk, spotOnFocus.Position, Quaternion.identity);
            OnTowerCreated(tower, spotOnFocus);
        }
    }

    private void PlacingBuildSpot(Vector3 closestPointOnTerrain, Vector3 HandPosition)
    {
        if (buildingBlocksNumber <= 0)
            IncreaseBuildingBlocks();
        latestBuildSpotDirection = (HandPosition - closestPointOnTerrain).normalized;
        
        for (int i = 0; i < buildingBlocksNumber; i++)
        {
            buildBlocks[i].transform.LookAt(HandPosition);
            buildBlocks[i].transform.position = closestPointOnTerrain + i * latestBuildSpotDirection * 3;
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
                var block = Instantiate(BuildSpotBlockPrefab);
                buildBlocks[buildingBlocksNumber - 1] = block;
            }
        }
    }
}
