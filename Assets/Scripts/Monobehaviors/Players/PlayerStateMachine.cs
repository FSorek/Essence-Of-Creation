using Data.Data_Types.Enums;
using Data.ScriptableObjects.Player;
using DataBehaviors.Player.States;
using DataBehaviors.StateMachines;
using UnityEngine;

namespace Monobehaviors.Players
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerStateData stateData;
        [SerializeField] private PlayerBuildData buildData;
        
        private StateMachine<PlayerStates> stateMachine;
        public void Awake()
        {
            stateMachine = new StateMachine<PlayerStates>(stateData);

            var awaitBuild = new AwaitBuildPlayerState(playerInput, buildData, stateData);
            var forging = new ForgingPlayerState(playerInput, buildData, stateData);
            var extracting = new ExtractingPlayerState(playerInput, buildData, stateData);
            var placeObelisk = new PlaceObeliskPlayerState(playerInput, buildData, stateData);
            var weaveEssence = new WeaveEssencePlayerState(playerInput, buildData, stateData);
            
            stateMachine.RegisterState(PlayerStates.AWAIT_BUILD, awaitBuild);
            stateMachine.RegisterState(PlayerStates.FORGING, forging);
            stateMachine.RegisterState(PlayerStates.EXTRACTING, extracting);
            stateMachine.RegisterState(PlayerStates.WEAVE_ESSENCE, weaveEssence);
            stateMachine.RegisterState(PlayerStates.PLACE_OBELISK, placeObelisk);

            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
            
            playerInput.OnFirePressed += PlayerInputOnFirePressed;
            playerInput.OnAirPressed += PlayerInputOnAirPressed;
            playerInput.OnWaterPressed += PlayerInputOnWaterPressed;
            playerInput.OnEarthPressed += PlayerInputOnEarthPressed;
            playerInput.OnStartPlacingObeliskPressed += PlayerInputOnPlaceObeliskPressed;
            playerInput.OnWeaveEssencePressed += PlayerInputOnWeaveEssencePressed;
        }

        private void PlayerInputOnWeaveEssencePressed()
        {
            if (buildData.ExtractedEssences.Count > 0)
                stateData.ChangeState(PlayerStates.WEAVE_ESSENCE);
        }

        private void PlayerInputOnPlaceObeliskPressed()
        {
            stateData.ChangeState(PlayerStates.PLACE_OBELISK);
        }

        private void PlayerInputOnEarthPressed()
        {
            buildData.CurrentEssence = buildData.EarthEssencePrefab;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnWaterPressed()
        {
            buildData.CurrentEssence = buildData.WaterEssencePrefab;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnAirPressed()
        {
            buildData.CurrentEssence = buildData.AirEssencePrefab;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnFirePressed()
        {
            buildData.CurrentEssence = buildData.FireEssencePrefab;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void Update()
        {
            stateMachine.Tick();
        }
    }
}