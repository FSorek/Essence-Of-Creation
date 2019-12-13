using System;
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
        public event Action<PlayerStates> OnStateEntered = delegate{  };
        public event Action<PlayerStates> OnStateExit = delegate { };
        public PlayerStates CurrentState => currentState;

        public void Awake()
        {
            player = GetComponent<PlayerComponent>();
            availablePlayerStates = new Dictionary<PlayerStates, PlayerState>
            {
                {PlayerStates.AWAIT_BUILD, new AwaitBuildPlayerState(player, this)}, // default state
                {PlayerStates.FORGING, new ForgingPlayerState(player, this)},
                {PlayerStates.PLACE_OBELISK, new PlaceObeliskPlayerState(player, this)},
                {PlayerStates.WEAVE_ESSENCE, new MergeAttackPlayerState(player, this)}
            };
        }

        private void Update()
        {
            if (currentPlayerState == null)
                ChangeState(availablePlayerStates.Keys.First());
            currentPlayerState.ListenToState();
        }

        public void ChangeState(PlayerStates state)
        {
            currentPlayerState?.OnStateExit();
            OnStateExit(currentState);
            currentState = state;
            currentPlayerState = availablePlayerStates[state];
            currentPlayerState.OnStateEnter();
            OnStateEntered(state);
        }
    }
}