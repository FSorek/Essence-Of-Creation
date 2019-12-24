using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using Monobehaviors.Tower.Attack;

namespace DataBehaviors.Player
{
    public class WeaveEssencePlayerState : IState
    {
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private readonly PlayerStateData stateData;

        public WeaveEssencePlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData)
        {
            this.input = input;
            this.buildData = buildData;
            this.stateData = stateData;
            
            input.OnWeaveEssencePressed += InputOnWeaveEssencePressed;
        }

        private void InputOnWeaveEssencePressed()
        {
            if (stateData.CurrentState != PlayerStates.AWAIT_BUILD)
                stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        public void StateEnter()
        {
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed += PlayerInputOnSecondaryKeyPressed;
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            if(buildData.ExtractedEssences.Count <= 0)
                return;
            
            var handPos = buildData.ConstructorObject.position;
            var buildSpots = buildData.AttractionSpots.Items.ToArray();
            var buildRange = buildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            
            var attractionSpot = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).GetComponent<AttractionSpot>();
            var essence = buildData.ExtractedEssences.Last();
            
            essence.transform.position = attractionSpot.transform.position;
            essence.transform.SetParent(null);
            attractionSpot.AssignEssence(essence);
            buildData.ExtractedEssences.Remove(essence);
            
            var attacks = essence.GetComponents<AttackComponent>();
            for (int i = 0; i < attacks.Length; i++)
            {
                attacks[i].enabled = true;
            }
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

        public void ListenToState()
        {
            if (buildData.ExtractedEssences.Count >= 2)
            {
                // check if can be merged then merge
            }
        }

        public void StateExit()
        {
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed -= PlayerInputOnSecondaryKeyPressed;
        }
    }
}