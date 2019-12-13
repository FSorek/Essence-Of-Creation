﻿using System;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
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
        [SerializeField] private PlayerStateData stateData;
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerBuildData buildData;

        [SerializeField] private ObjectPool buildAirPool;
        [SerializeField] private ObjectPool buildEarthPool;
        [SerializeField] private ObjectPool buildFirePool;
        [SerializeField] private ObjectPool buildWaterPool;
        private ObjectPool lastPoolUsed;
        private BuildEffect activeEffect;

        private void Awake()
        {
            stateData.OnStateEntered += PlayerStateMachineOnStateEntered;
            stateData.OnStateExit += PlayerStateMachineOnStateExit;
            input.OnFirePressed += PlayerInputOnFirePressed;
            input.OnAirPressed += PlayerInputOnAirPressed;
            input.OnWaterPressed += PlayerInputOnWaterPressed;
            input.OnEarthPressed += PlayerInputOnEarthPressed;
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
            if (buildData.TargetAttraction == null) return;
            SetTargetedPosition(buildData.TargetAttraction.transform.position);
            Invoke(nameof(StopEffect), buildData.BuildTime);
        }

        private void PlayerInputOnEarthPressed()
        {
            StopEffect();
            activeEffect = buildEarthPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = buildEarthPool;
        }

        private void PlayerInputOnWaterPressed()
        {
            StopEffect();
            activeEffect = buildWaterPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = buildWaterPool;
        }

        private void PlayerInputOnAirPressed()
        {
            StopEffect();
            activeEffect = buildAirPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = buildAirPool;
        }

        private void PlayerInputOnFirePressed()
        {
            StopEffect();
            activeEffect = buildFirePool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = buildFirePool;
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
            activeEffect.SetFollowedTransform(buildData.ConstructorObject, position);
        }
    }
}