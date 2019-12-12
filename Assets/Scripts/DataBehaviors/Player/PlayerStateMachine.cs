using System.Collections.Generic;
using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;

namespace DataBehaviors.Player
{
    public class PlayerStateMachine
    {
        private readonly PlayerComponent player;
        private readonly Dictionary<PlayerStates, PlayerState> availablePlayerStates;
        private PlayerState currentPlayerState;
        private PlayerState lastPlayerState;

        public PlayerStateMachine(PlayerComponent player)
        {
            this.player = player;
            availablePlayerStates = new Dictionary<PlayerStates, PlayerState>
            {
                {PlayerStates.ATTUNED, new AttunedPlayerState(player, EconomyManager.Instance)},
                {PlayerStates.PLACE_BUILD_SPOT, new PlacingBuildSpotPlayerState(player)},
                {PlayerStates.WEAVE_ESSENCE, new MergeAttackPlayerState(player)}
            };
            currentPlayerState = availablePlayerStates[PlayerStates.WEAVE_ESSENCE];
            this.player.OnElementExecuted += ChangeState;
        }

        private void ChangeState(Elements element)
        {
            switch (element)
            {
                case Elements.Fire:
                    currentPlayerState = availablePlayerStates[PlayerStates.ATTUNED];
                    break;
                case Elements.Earth:
                    currentPlayerState = availablePlayerStates[PlayerStates.ATTUNED];
                    break;
                case Elements.Water:
                    currentPlayerState = availablePlayerStates[PlayerStates.ATTUNED];
                    break;
                case Elements.Air:
                    currentPlayerState = availablePlayerStates[PlayerStates.ATTUNED];
                    break;
                case Elements.Invocation:
                    currentPlayerState = availablePlayerStates[PlayerStates.PLACE_BUILD_SPOT];
                    break;
                default:
                    currentPlayerState = availablePlayerStates[PlayerStates.WEAVE_ESSENCE];
                    break;
            }
        }

        public void Tick()
        {
            if (currentPlayerState != null)
            {
                if (lastPlayerState != currentPlayerState)
                {
                    lastPlayerState?.OnStateExit();
                    lastPlayerState = currentPlayerState;
                }

                currentPlayerState.ListenToState();
            }
        }
    }
}