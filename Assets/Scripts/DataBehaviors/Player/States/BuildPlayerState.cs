using System.Linq;
using Data.Data_Types;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class BuildPlayerState : PlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        private readonly PlayerBuildData buildData;
        private Vector3 latestBuildSpotDirection;
        private AttractionSpot spotOnFocus;
        private bool isBuilding;
        private GameObject currentEssence;
        private float timeStarted;
        private AttractionSpot targetAttractionSpot;

        public BuildPlayerState(PlayerComponent player, PlayerStateMachine stateMachine) : base(player)
        {
            this.stateMachine = stateMachine;
            buildData = player.BuildData;
        }

        private void PlayerInputOnPlaceObeliskPressed()
        {
            stateMachine.ChangeState(PlayerStates.PLACE_OBELISK);
        }

        public override PlayerStates ListenToState()
        {
            if(!isBuilding) return PlayerStates.BUILD;
            
            if (Time.time - timeStarted > player.BuildData.BuildTime)
            {
                ForgeEssence();
                // Fire OnForgeFinished event?
            }

            return PlayerStates.BUILD;
        }

        public override void OnStateExit()
        {
            isBuilding = false;
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
            player.PlayerInput.OnPrimaryKeyReleased += PlayerInputOnPrimaryKeyReleased;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            isBuilding = false;
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            // <check if enough essence to build>
            var handPos = player.HandTransform.transform.position;
            var buildSpots = AttractionSpot.AttractionSpots.ToArray();
            var buildRange = player.BuildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            targetAttractionSpot = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).GetComponent<AttractionSpot>();
            
            if (targetAttractionSpot != null)
            {
                timeStarted = Time.time;
                isBuilding = true;
            }
        }

        private void PlayerInputOnEarthPressed()
        {

            currentEssence = buildData.EarthEssencePrefab;
            RefreshState();
        }


        private void PlayerInputOnWaterPressed()
        {
            currentEssence = buildData.WaterEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnAirPressed()
        {
            currentEssence = buildData.AirEssencePrefab;
            RefreshState();
        }

        private void PlayerInputOnFirePressed()
        {
            currentEssence = buildData.FireEssencePrefab;
            RefreshState();
        }

        private void ForgeEssence() //to-do: pool
        {
            var essence = GameObject.Instantiate(currentEssence, targetAttractionSpot.transform.position, Quaternion.identity);
            targetAttractionSpot.AssignEssence(essence);
            isBuilding = false;
        }
        private void RefreshState()
        {
            if(stateMachine.CurrentState != PlayerStates.BUILD)
                stateMachine.ChangeState(PlayerStates.BUILD);
            timeStarted = Time.time;
        }
    }
}
