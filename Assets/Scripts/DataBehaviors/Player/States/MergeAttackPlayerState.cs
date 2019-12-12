using System;
using System.Linq;
using Data.Interfaces.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class MergeAttackPlayerState : PlayerState
    {
        private BuildSpotComponent currentBuildSpotComponent;
        private float timeStarted;

        public MergeAttackPlayerState(PlayerComponent playerC) : base(playerC)
        {
        }

        public static event Action<BuildSpotComponent> OnExtractTowerStarted = delegate { };
        public static event Action<BuildSpotComponent> OnExtractTowerFinished = delegate { };
        public static event Action OnExtractTowerInterrupted = delegate { };

        public static event Action<BuildSpotComponent> OnAssembleTowerStarted = delegate { };
        public static event Action<BuildSpotComponent> OnAssembleTowerFinished = delegate { };
        public static event Action OnAssembleTowerInterrupted = delegate { };

        public static event Action OnMiddleMouseAttack = delegate { };

        public override void ListenToState()
        {
            ///-------- Left Mouse

            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                var potentialTargets = RangeTargetScanner.GetTargets(playerComponentC.HandTransform.transform.position,
                    BuildSpotComponent.BuildSpots.ToArray(),
                    playerComponentC.BuildingData.BuildSpotDetectionRange)?.Where(t => !t.GetComponent<BuildSpotComponent>().IsOccupied).ToArray();
                currentBuildSpotComponent =
                    ClosestEntityFinder.GetClosestTransform(potentialTargets, playerComponentC.HandTransform.position).GetComponent<BuildSpotComponent>();
                if (currentBuildSpotComponent != null)
                {
                    OnAssembleTowerStarted(currentBuildSpotComponent);
                    timeStarted = Time.time;
                }
            }

            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                if (Time.time - timeStarted > playerComponentC.BuildingData.BuildTime && currentBuildSpotComponent != null)
                {
                    OnAssembleTowerFinished(currentBuildSpotComponent);
                    currentBuildSpotComponent = null;
                }

            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
                if (Time.time - timeStarted < playerComponentC.BuildingData.BuildTime)
                    OnAssembleTowerInterrupted();

            ///-------- Right Mouse

            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
            {
                var potentialTargets = RangeTargetScanner.GetTargets(playerComponentC.HandTransform.transform.position,
                        BuildSpotComponent.BuildSpots.ToArray(),
                        playerComponentC.BuildingData.BuildSpotDetectionRange)?.Where(t => t.GetComponent<BuildSpotComponent>().IsOccupied)
                    .ToArray();
                if(potentialTargets == null)
                    return;
                currentBuildSpotComponent =
                    ClosestEntityFinder.GetClosestTransform(potentialTargets, playerComponentC.HandTransform.position).GetComponent<BuildSpotComponent>();
                if (currentBuildSpotComponent != null)
                {
                    OnExtractTowerStarted(currentBuildSpotComponent);
                    timeStarted = Time.time;
                }
            }

            if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
                if (Time.time - timeStarted > playerComponentC.BuildingData.BuildTime && currentBuildSpotComponent != null)
                {
                    OnExtractTowerFinished(currentBuildSpotComponent);
                    currentBuildSpotComponent = null;
                }

            if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
                if (Time.time - timeStarted < playerComponentC.BuildingData.BuildTime)
                    OnExtractTowerInterrupted();

            ///-------- Middle Mouse
            if (Input.GetMouseButton(2)) OnMiddleMouseAttack();
        }

        public override void OnStateExit()
        {
        }
    }
}