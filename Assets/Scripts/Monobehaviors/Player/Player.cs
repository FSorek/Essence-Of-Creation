using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerBuildingData buildingData;
    [SerializeField] private GameObject Hand;
    [SerializeField] private PlayerVFXData vfxData;
    public event Action<Elements> OnElementExecuted = delegate { };

    private IInputProcessor mouseKeyboardInput;
    private Elements currentElement;
    private PlayerStateMachine stateMachine;
    private PlayerState currentPlayerState;
    private SummonEffectController summonEffectController;


    private void Awake()
    {
        mouseKeyboardInput = new MouseKeyboardInput();
        currentElement = Elements.None;
        stateMachine = new PlayerStateMachine(this);
        summonEffectController = new SummonEffectController(this);
    }

    private void Update()
    {
        mouseKeyboardInput.GlobalControls();
        if (mouseKeyboardInput.CurrentElement != currentElement)
        {
            OnElementExecuted(mouseKeyboardInput.CurrentElement);
            currentElement = mouseKeyboardInput.CurrentElement;
        }
        stateMachine.Tick();
    }

    public Transform HandTransform => Hand.transform;
    public PlayerState CurrentPlayerState => currentPlayerState;
    public Elements CurrentElement => currentElement;

    public PlayerVFXData VFXData => vfxData;
    public PlayerBuildingData BuildingData => buildingData;
}
