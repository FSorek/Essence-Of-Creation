using System;
using System.Linq;
using UnityEngine;

public class AttunedPlayerState : PlayerState
{
    public static event Action<PlayerController, BuildSpot> OnElementBuildingStarted;
    public static event Action<PlayerController> OnElementBuildingFinished;
    public static event Action<PlayerController> OnElementBuildingInterrupted;

    private BuildSpot currentBuildSpot;
    private float timeStarted;

    public AttunedPlayerState(PlayerController playerC) : base(playerC)
    {
    }

    public override void ListenToState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var potentialTargets = RangeTargetScanner<BuildSpot>.GetTargets(playerC.HandTransform.transform.position, BuildSpotManager.BuildSpots.ToArray(),
                playerC.BuildingData.BuildSpotDetectionRange)?.Where(t => !t.IsOccupied).ToArray();
            currentBuildSpot = ClosestEntityFinder<BuildSpot>.GetClosestTransform(potentialTargets, playerC.HandTransform.position);
            if(currentBuildSpot != null)
            {
                OnElementBuildingStarted(playerC, currentBuildSpot);
                timeStarted = Time.time;
            }
        }
        if(Input.GetMouseButton(0))
        if (Time.time - timeStarted > playerC.BuildingData.BuildTime && currentBuildSpot != null)
        {
            currentBuildSpot = null;
            OnElementBuildingFinished(playerC);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(Time.time - timeStarted < playerC.BuildingData.BuildTime)
                OnElementBuildingInterrupted(playerC);
        }
    }

    public override void OnStateExit()
    {
        
    }
}
