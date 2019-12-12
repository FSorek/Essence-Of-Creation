using System;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Input;
using DataBehaviors.Player;
using DataBehaviors.Player.Particles;
using DefaultNamespace;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class PlayerComponent : MonoBehaviour, IPlayer
    {
        [SerializeField] private GameObject Hand;

        private IInputProcessor mouseKeyboardInput;
        private PlayerStateMachine stateMachine;
        private SummonEffectController summonEffectController;

        public PlayerState CurrentPlayerState { get; }

        public event Action<Elements> OnElementExecuted = delegate { };

        public Transform HandTransform => Hand.transform;
        public Elements CurrentElement { get; private set; }

        public PlayerVFXData VFXData;

        public PlayerBuildingData BuildingData;


        private void Awake()
        {
            mouseKeyboardInput = new MouseKeyboardInput();
            CurrentElement = Elements.None;
            stateMachine = new PlayerStateMachine(this);
            summonEffectController = new SummonEffectController(this);
        }

        private void Update()
        {
            mouseKeyboardInput.GlobalControls();
            if (mouseKeyboardInput.CurrentElement != CurrentElement)
            {
                OnElementExecuted(mouseKeyboardInput.CurrentElement);
                CurrentElement = mouseKeyboardInput.CurrentElement;
            }

            stateMachine.Tick();
        }
    }
}