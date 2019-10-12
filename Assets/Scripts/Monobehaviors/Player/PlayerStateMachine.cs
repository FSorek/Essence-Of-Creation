using System;
using System.Collections.Generic;

public class PlayerStateMachine
{
    private readonly IPlayer player;
    private PlayerState currentPlayerState;
    private PlayerState lastPlayerState;
    private Dictionary<PlayerStates, PlayerState> availablePlayerStates;

    public PlayerStateMachine(IPlayer player)
    {
        this.player = player;
        availablePlayerStates = new Dictionary<PlayerStates, PlayerState>()
        {
            {PlayerStates.ATTUNED, new AttunedPlayerState(player, EconomyManager.Instance)},
            {PlayerStates.PLACE_BUILD_SPOT, new PlacingBuildSpotPlayerState(player)},
            {PlayerStates.WEAVE_ESSENCE, new MergeAttackPlayerState(player) }
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
