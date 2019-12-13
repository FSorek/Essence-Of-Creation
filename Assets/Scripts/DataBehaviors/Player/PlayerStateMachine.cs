using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerStateData stateData;
        [SerializeField] private PlayerBuildData buildData;
        
        private Dictionary<PlayerStates, PlayerState> availablePlayerStates;
        private PlayerState currentPlayerState;
        public void Awake()
        {
            availablePlayerStates = new Dictionary<PlayerStates, PlayerState>
            {
                {PlayerStates.AWAIT_BUILD, new AwaitBuildPlayerState(playerInput, buildData, stateData)},
                {PlayerStates.FORGING, new ForgingPlayerState(playerInput, buildData, stateData)},
                {PlayerStates.PLACE_OBELISK, new PlaceObeliskPlayerState(buildData, playerInput, stateData)},
            };
            
            stateData.OnStateEntered += StateDataOnStateEntered;
            stateData.OnStateExit += StateDataOnStateExit;
            stateData.ChangeState(availablePlayerStates.First().Key);
        }

        private void StateDataOnStateExit(PlayerStates state)
        {
            currentPlayerState?.OnStateExit();
        }

        private void StateDataOnStateEntered(PlayerStates state)
        {
            currentPlayerState = availablePlayerStates[state];
            currentPlayerState.OnStateEnter();
        }

        private void Update()
        {
            if (currentPlayerState == null)
                currentPlayerState = availablePlayerStates[stateData.CurrentState];
            currentPlayerState.ListenToState();
        }

    }
}