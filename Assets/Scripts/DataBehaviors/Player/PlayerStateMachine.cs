using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Game;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player
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
            var placeObelisk = new PlaceObeliskPlayerState(buildData, playerInput, stateData);

            stateMachine.RegisterState(PlayerStates.AWAIT_BUILD, awaitBuild);
            stateMachine.RegisterState(PlayerStates.FORGING, forging);
            stateMachine.RegisterState(PlayerStates.PLACE_OBELISK, placeObelisk);

            
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void Update()
        {
            stateMachine.Tick();
        }
    }
}