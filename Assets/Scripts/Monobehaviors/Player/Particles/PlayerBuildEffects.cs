using System;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Game.Entity.Targeting;
using DataBehaviors.Player;
using DataBehaviors.Player.States;
using Monobehaviors.BuildSpot;
using Monobehaviors.Pooling;
using UnityEngine;

namespace Monobehaviors.Player.Particles
{
    public class PlayerBuildEffects : MonoBehaviour
    {
        private BuildEffect activeEffect;
        [SerializeField] private ObjectPool BuildAirPool;
        [SerializeField] private ObjectPool BuildEarthPool;
        [SerializeField] private ObjectPool BuildFirePool;
        [SerializeField] private ObjectPool BuildWaterPool;
        private PlayerComponent player;
        private PlayerStateMachine playerStateMachine;
        private ObjectPool lastPoolUsed;

        private void Awake()
        {
            player = GetComponent<PlayerComponent>();
            playerStateMachine = GetComponent<PlayerStateMachine>();
            playerStateMachine.OnStateEntered += PlayerStateMachineOnStateEntered;
            playerStateMachine.OnStateExit += PlayerStateMachineOnStateExit;
            player.PlayerInput.OnFirePressed += PlayerInputOnFirePressed;
            player.PlayerInput.OnAirPressed += PlayerInputOnAirPressed;
            player.PlayerInput.OnWaterPressed += PlayerInputOnWaterPressed;
            player.PlayerInput.OnEarthPressed += PlayerInputOnEarthPressed;
        }

        private void PlayerStateMachineOnStateExit(PlayerStates state)
        {
            if(state != PlayerStates.FORGING) return;
                StopEffect();
        }

        private void PlayerStateMachineOnStateEntered(PlayerStates state)
        {
            if(state != PlayerStates.FORGING) return;
            CancelInvoke();
            if (player.BuildData.TargetAttraction == null) return;
            SetTargetedPosition(player.BuildData.TargetAttraction.transform.position);
            Invoke(nameof(StopEffect), player.BuildData.BuildTime);
        }

        private void PlayerInputOnEarthPressed()
        {
            StopEffect();
            activeEffect = BuildEarthPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildEarthPool;
        }

        private void PlayerInputOnWaterPressed()
        {
            StopEffect();
            activeEffect = BuildWaterPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildWaterPool;
        }

        private void PlayerInputOnAirPressed()
        {
            StopEffect();
            activeEffect = BuildAirPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildAirPool;
        }

        private void PlayerInputOnFirePressed()
        {
            StopEffect();
            activeEffect = BuildFirePool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildFirePool;
        }

        private void StopEffect()
        {
            if (activeEffect != null)
            {
                activeEffect.Detach();
                activeEffect = lastPoolUsed.Get().GetComponent<BuildEffect>();
            }
        }
 
        private void SetTargetedPosition(Vector3 position)
        {
            if (activeEffect == null) return;
            activeEffect.SetFollowedTransform(player.HandTransform, position);
        }
    }
}