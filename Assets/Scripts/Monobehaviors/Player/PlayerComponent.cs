using System;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.PlayerInput;
using DataBehaviors.Player;
using DataBehaviors.Player.Particles;
using DefaultNamespace;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject Hand;
        private IPlayerInput playerInput = new MouseKeyboardPlayerInput();
        private SummonEffectController summonEffectController;
        private Transform handTransform;


        public Transform HandTransform => handTransform;
        public IPlayerInput PlayerInput => playerInput;
        public PlayerVFXData VFXData;
        public PlayerBuildData BuildData;


        private void Awake()
        {
            handTransform = Hand.GetComponent<Transform>();
            summonEffectController = new SummonEffectController(this);
        }

        private void Update()
        {
            playerInput.Tick();
        }
    }
}