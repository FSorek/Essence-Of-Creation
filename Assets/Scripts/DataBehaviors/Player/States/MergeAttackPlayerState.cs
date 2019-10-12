using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class MergeAttackPlayerState : PlayerState
{
    public static event Action<BuildSpot> OnExtractTowerStarted = delegate { };
    public static event Action<BuildSpot> OnExtractTowerFinished = delegate { };
    public static event Action OnExtractTowerInterrupted = delegate { };

    public static event Action<BuildSpot> OnAssembleTowerStarted = delegate { };
    public static event Action<BuildSpot> OnAssembleTowerFinished = delegate { };
    public static event Action OnAssembleTowerInterrupted = delegate { };

    public static event Action OnMiddleMouseAttack = delegate { };

    private BuildSpot currentBuildSpot;
    private float timeStarted;

    public MergeAttackPlayerState(IPlayer playerC) : base(playerC)
    {
    }

    public override void ListenToState()
    {
        ///-------- Left Mouse

        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            var potentialTargets = RangeTargetScanner<BuildSpot>.GetTargets(playerC.HandTransform.transform.position, BuildSpot.BuildSpots.ToArray(),
                playerC.BuildingData.BuildSpotDetectionRange)?.Where(t => !t.IsOccupied).ToArray();
            currentBuildSpot = ClosestEntityFinder<BuildSpot>.GetClosestTransform(potentialTargets, playerC.HandTransform.position);
            if (currentBuildSpot != null)
            {
                OnAssembleTowerStarted(currentBuildSpot);
                timeStarted = Time.time;
            }
        }
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            if (Time.time - timeStarted > playerC.BuildingData.BuildTime && currentBuildSpot != null)
            {
                OnAssembleTowerFinished(currentBuildSpot);
                currentBuildSpot = null;
            }
        if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            if (Time.time - timeStarted < playerC.BuildingData.BuildTime)
                OnAssembleTowerInterrupted();
        }

        ///-------- Right Mouse

        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
        {
            var potentialTargets = RangeTargetScanner<BuildSpot>.GetTargets(playerC.HandTransform.transform.position, BuildSpot.BuildSpots.ToArray(),
                playerC.BuildingData.BuildSpotDetectionRange)?.Where(t => t.GetComponent<BuildSpot>().IsOccupied).ToArray();
            currentBuildSpot = ClosestEntityFinder<BuildSpot>.GetClosestTransform(potentialTargets, playerC.HandTransform.position);
            if (currentBuildSpot != null)
            {
                OnExtractTowerStarted(currentBuildSpot);
                timeStarted = Time.time;
            }
        }
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
            if (Time.time - timeStarted > playerC.BuildingData.BuildTime && currentBuildSpot != null)
            {
                OnExtractTowerFinished(currentBuildSpot);
                currentBuildSpot = null;
            }
        if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
        {
            if (Time.time - timeStarted < playerC.BuildingData.BuildTime)
                OnExtractTowerInterrupted();
        }

        ///-------- Middle Mouse
        if (Input.GetMouseButton(2))
        {
            OnMiddleMouseAttack();
        }

    }

    public override void OnStateExit()
    {
        
    }
}

