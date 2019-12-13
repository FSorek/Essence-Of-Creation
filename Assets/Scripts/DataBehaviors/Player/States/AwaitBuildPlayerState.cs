using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class AwaitBuildPlayerState : PlayerState
    {
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private Vector3 latestBuildSpotDirection;
        private AttractionSpot spotOnFocus;
        public AwaitBuildPlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData) : base(stateData)
        {
            this.input = input;
            this.buildData = buildData;
        }

        private void PlayerInputOnPlaceObeliskPressed()
        {
            stateData.ChangeState(PlayerStates.PLACE_OBELISK);
        }

        public override void ListenToState()
        {
            
        }

        public override void OnStateExit()
        {
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            input.OnStartPlacingObeliskPressed -= PlayerInputOnPlaceObeliskPressed;
        }

        public override void OnStateEnter()
        {
            input.OnFirePressed += PlayerInputOnFirePressed;
            input.OnAirPressed += PlayerInputOnAirPressed;
            input.OnWaterPressed += PlayerInputOnWaterPressed;
            input.OnEarthPressed += PlayerInputOnEarthPressed;
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnStartPlacingObeliskPressed += PlayerInputOnPlaceObeliskPressed;
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            var handPos = buildData.ConstructorObject.position;
            var buildSpots = AttractionSpot.AttractionSpots.ToArray();
            var buildRange = buildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            buildData.TargetAttraction = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).GetComponent<AttractionSpot>();

            stateData.ChangeState(PlayerStates.FORGING);
        }

        private void PlayerInputOnEarthPressed()
        {

            buildData.CurrentEssence = buildData.EarthEssencePrefab;
            RefreshState();
        }


        private void PlayerInputOnWaterPressed()
        {
            buildData.CurrentEssence = buildData.WaterEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnAirPressed()
        {
            buildData.CurrentEssence = buildData.AirEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnFirePressed()
        {
            buildData.CurrentEssence = buildData.FireEssencePrefab;
            RefreshState();
        }
        private void RefreshState()
        {
            if(stateData.CurrentState != PlayerStates.AWAIT_BUILD)
                stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }
    }
}
