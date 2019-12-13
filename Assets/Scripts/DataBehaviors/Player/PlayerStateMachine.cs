using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerComponent player;
        private Dictionary<PlayerStates, PlayerState> availablePlayerStates;
        private PlayerState currentPlayerState;
        private PlayerStates currentState;
        public PlayerStates CurrentState => currentState;

        public void Awake()
        {
            player = GetComponent<PlayerComponent>();
            availablePlayerStates = new Dictionary<PlayerStates, PlayerState>
            {
                {PlayerStates.BUILD, new BuildPlayerState(player, this)}, // default state
                {PlayerStates.PLACE_OBELISK, new PlaceObeliskPlayerState(player, this)},
                {PlayerStates.WEAVE_ESSENCE, new MergeAttackPlayerState(player, this)}
            };
        }

        private void Update()
        {
            if (currentPlayerState == null)
                ChangeState(availablePlayerStates.Keys.First());

            var nextState = currentPlayerState.ListenToState();

            if (availablePlayerStates[nextState] != currentPlayerState)
            {
                ChangeState(nextState);
            }
        }

        public void ChangeState(PlayerStates state)
        {
            currentPlayerState?.OnStateExit();
            currentState = state;
            currentPlayerState = availablePlayerStates[state];
            currentPlayerState.OnStateEnter();
        }
    }
}