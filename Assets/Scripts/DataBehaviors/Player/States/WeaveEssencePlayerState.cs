using System.Collections.Generic;
using System.Linq;
using Data.Data_Types.Enums;
using Data.Interfaces.StateMachines;
using Data.ScriptableObjects.Player;
using DataBehaviors.Game.Systems;
using DataBehaviors.Game.Targeting;
using Monobehaviors.AttractionSpots;
using Monobehaviors.Essences;
using Monobehaviors.Essences.Attacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class WeaveEssencePlayerState : IState
    {
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private readonly PlayerStateData stateData;
        private readonly RecipeData recipes;

        public WeaveEssencePlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData, RecipeData recipes)
        {
            this.input = input;
            this.buildData = buildData;
            this.stateData = stateData;
            this.recipes = recipes;
        }

        public void StateEnter()
        {
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed += PlayerInputOnSecondaryKeyPressed;
            
            if (buildData.ExtractedEssences == null || buildData.ExtractedEssences.Count < 2) 
                return;
            
            var firstEssence = buildData.ExtractedEssences[0].GetComponent<Essence>();
            var secondEssence = buildData.ExtractedEssences[1].GetComponent<Essence>();
            var result = recipes.TryMerge(firstEssence, secondEssence);
            if(result == null) return;
            
                
            for (int i = 0; i < buildData.ExtractedEssences.Count; i++)
            {
                Object.Destroy(buildData.ExtractedEssences[i]);
            }
            
            var resultObj = Object.Instantiate(result.gameObject, buildData.ConstructorObject.position + Vector3.down * 3f,
                Quaternion.identity, buildData.ConstructorObject);
            resultObj.GetComponent<Essence>().Deactivate();

            buildData.ExtractedEssences.Clear();
            buildData.ExtractedEssences.Add(resultObj);
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
            
            attractionSpot.AssignEssence(essence);
            buildData.ExtractedEssences.Remove(essence);
            
            essence.GetComponent<Essence>().Activate();
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

        }

        public void StateExit()
        {
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed -= PlayerInputOnSecondaryKeyPressed;
        }
    }
}