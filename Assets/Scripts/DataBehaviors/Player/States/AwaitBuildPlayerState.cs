using System.Linq;
using Data.Data_Types;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class AwaitBuildPlayerState : PlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        private readonly PlayerBuildData buildData;
        private Vector3 latestBuildSpotDirection;
        private AttractionSpot spotOnFocus;
        public AwaitBuildPlayerState(PlayerComponent player, PlayerStateMachine stateMachine) : base(player)
        {
            this.stateMachine = stateMachine;
            buildData = player.BuildData;
        }

        private void PlayerInputOnPlaceObeliskPressed()
        {
            stateMachine.ChangeState(PlayerStates.PLACE_OBELISK);
        }

        public override void ListenToState()
        {
            
        }

        public override void OnStateExit()
        {
            player.PlayerInput.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            player.PlayerInput.OnStartPlacingObeliskPressed -= PlayerInputOnPlaceObeliskPressed;
        }

        public override void OnStateEnter()
        {
            player.PlayerInput.OnFirePressed += PlayerInputOnFirePressed;
            player.PlayerInput.OnAirPressed += PlayerInputOnAirPressed;
            player.PlayerInput.OnWaterPressed += PlayerInputOnWaterPressed;
            player.PlayerInput.OnEarthPressed += PlayerInputOnEarthPressed;
            player.PlayerInput.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            player.PlayerInput.OnStartPlacingObeliskPressed += PlayerInputOnPlaceObeliskPressed;
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            var handPos = player.HandTransform.transform.position;
            var buildSpots = AttractionSpot.AttractionSpots.ToArray();
            var buildRange = player.BuildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            player.BuildData.TargetAttraction = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).GetComponent<AttractionSpot>();

            stateMachine.ChangeState(PlayerStates.FORGING);
        }

        private void PlayerInputOnEarthPressed()
        {

            player.BuildData.CurrentEssence = buildData.EarthEssencePrefab;
            RefreshState();
        }


        private void PlayerInputOnWaterPressed()
        {
            player.BuildData.CurrentEssence = buildData.WaterEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnAirPressed()
        {
            player.BuildData.CurrentEssence = buildData.AirEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnFirePressed()
        {
            player.BuildData.CurrentEssence = buildData.FireEssencePrefab;
            RefreshState();
        }
        private void RefreshState()
        {
            if(stateMachine.CurrentState != PlayerStates.AWAIT_BUILD)
                stateMachine.ChangeState(PlayerStates.AWAIT_BUILD);
        }
    }
}
