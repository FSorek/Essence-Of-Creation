using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class AwaitBuildPlayerState : IState
    {
        private readonly PlayerStateData stateData;
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private Vector3 latestBuildSpotDirection;
        private AttractionSpot spotOnFocus;
        public AwaitBuildPlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData)
        {
            this.input = input;
            this.buildData = buildData;
            this.stateData = stateData;
        }

        public void ListenToState()
        {
            
        }

        public void StateExit()
        {
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed -= PlayerInputOnSecondaryKeyPressed;
        }

        public void StateEnter()
        {
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed += PlayerInputOnSecondaryKeyPressed;
        }
        private void PlayerInputOnPrimaryKeyPressed()
        {
            var handPos = buildData.ConstructorObject.position;
            var buildSpots = buildData.AttractionSpots.Items.ToArray();
            var buildRange = buildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            buildData.TargetAttraction = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).GetComponent<AttractionSpot>();

            stateData.ChangeState(PlayerStates.FORGING);
        }
        private void PlayerInputOnSecondaryKeyPressed()
        {
            var handPos = buildData.ConstructorObject.position;
            var buildSpots = buildData.AttractionSpots.Items.ToArray();
            var buildRange = buildData.BuildSpotDetectionRange;
            
            var occupiedSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(occupiedSpots.Length <= 0) return;
            buildData.TargetAttraction = ClosestEntityFinder.GetClosestTransform(occupiedSpots, handPos).GetComponent<AttractionSpot>();

            stateData.ChangeState(PlayerStates.EXTRACTING);
        }
    }
}
