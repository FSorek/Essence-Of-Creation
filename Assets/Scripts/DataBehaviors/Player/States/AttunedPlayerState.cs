using System;
using System.Linq;
using Data.Interfaces.Game.Economy;
using Data.Interfaces.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class AttunedPlayerState : PlayerState
    {
        private readonly IEconomyManager economyManager;

        private BuildSpotComponent currentBuildSpotComponent;
        private float timeStarted;

        public AttunedPlayerState(PlayerComponent playerComponentC, IEconomyManager economyManager) : base(playerComponentC)
        {
            this.economyManager = economyManager;
        }

        public static event Action<PlayerComponent, BuildSpotComponent> OnElementBuildingStarted;
        public static event Action<PlayerComponent> OnElementBuildingFinished;
        public static event Action<PlayerComponent> OnElementBuildingInterrupted;

        public override void ListenToState()
        {
            if (economyManager.Essence < economyManager.Settings.EssencePerSummon)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                var potentialTargets = RangeTargetScanner.GetTargets(playerComponentC.HandTransform.transform.position,
                    BuildSpotComponent.BuildSpots.ToArray(),
                    playerComponentC.BuildingData.BuildSpotDetectionRange)?.Where(t => !t.GetComponent<BuildSpotComponent>().IsOccupied).ToArray();
                currentBuildSpotComponent =
                    ClosestEntityFinder.GetClosestTransform(potentialTargets, playerComponentC.HandTransform.position).GetComponent<BuildSpotComponent>();
                if (currentBuildSpotComponent != null)
                {
                    OnElementBuildingStarted(playerComponentC, currentBuildSpotComponent);
                    timeStarted = Time.time;
                }
            }

            if (Input.GetMouseButton(0))
                if (Time.time - timeStarted > playerComponentC.BuildingData.BuildTime && currentBuildSpotComponent != null)
                {
                    currentBuildSpotComponent = null;
                    OnElementBuildingFinished(playerComponentC);
                }

            if (Input.GetMouseButtonUp(0))
                if (Time.time - timeStarted < playerComponentC.BuildingData.BuildTime)
                    OnElementBuildingInterrupted(playerComponentC);
        }

        public override void OnStateExit()
        {
        }
    }
}