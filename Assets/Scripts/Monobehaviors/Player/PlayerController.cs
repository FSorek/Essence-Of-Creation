using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerBuildManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerBuildingData buildingData;
    [SerializeField] private GameObject Hand;

    public PlayerBuildingData BuildingData => buildingData;
    public Transform HandTransform => Hand.transform;
    public PlayerState CurrentPlayerState => currentPlayerState;
    public Elements CurrentElement { get; private set; }

    public static event Action<Elements> OnElementExecuted;
    private PlayerState currentPlayerState;
    private PlayerState lastPlayerState;
    private Dictionary<Type, PlayerState> availablePlayerStates;

    private void Awake()
    {
        availablePlayerStates = new Dictionary<Type, PlayerState>()
        {
            {typeof(AttunedPlayerState), new AttunedPlayerState(this)},
            {typeof(PlacingBuildSpotPlayerState), new PlacingBuildSpotPlayerState(this)},
            {typeof(MergeAttackPlayerState), new MergeAttackPlayerState(this) }
        };
        currentPlayerState = availablePlayerStates[typeof(MergeAttackPlayerState)];
    }

    private void Update()
    {
        GlobalControls();
        if(currentPlayerState != null)
        {
            if (lastPlayerState != currentPlayerState)
            {
                lastPlayerState?.OnStateExit();
                lastPlayerState = currentPlayerState;
            }
            currentPlayerState.ListenToState();
        }
    }

    private void GlobalControls()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ExecuteElement(Elements.Fire);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExecuteElement(Elements.Air);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ExecuteElement(Elements.Water);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ExecuteElement(Elements.Earth);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ExecuteElement(Elements.Invocation);
        }
    }

    private void ExecuteElement(Elements element)
    {
        if (CurrentElement != element)
        {
            CurrentElement = element;
            OnElementExecuted(element);
            if(element != Elements.Invocation)
            {
                currentPlayerState = availablePlayerStates[typeof(AttunedPlayerState)];
            }
            else
            {
                currentPlayerState = availablePlayerStates[typeof(PlacingBuildSpotPlayerState)];
            }
        }
        else
        {
            CurrentElement = Elements.None;
            OnElementExecuted(Elements.None);
            currentPlayerState = availablePlayerStates[typeof(MergeAttackPlayerState)];
        }
    }
}
