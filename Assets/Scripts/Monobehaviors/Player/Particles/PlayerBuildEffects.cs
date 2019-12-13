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
            player.PlayerInput.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            player.PlayerInput.OnPrimaryKeyReleased += PlayerInputOnPrimaryKeyReleased;
            player.PlayerInput.OnFirePressed += PlayerInputOnFirePressed;
            player.PlayerInput.OnAirPressed += PlayerInputOnAirPressed;
            player.PlayerInput.OnWaterPressed += PlayerInputOnWaterPressed;
            player.PlayerInput.OnEarthPressed += PlayerInputOnEarthPressed;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            StopEffect();
        }

        private void PlayerInputOnEarthPressed()
        {
            activeEffect = BuildEarthPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildEarthPool;
        }

        private void PlayerInputOnWaterPressed()
        {
            activeEffect = BuildWaterPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildWaterPool;
        }

        private void PlayerInputOnAirPressed()
        {
            activeEffect = BuildAirPool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildAirPool;
        }

        private void PlayerInputOnFirePressed()
        {
            activeEffect = BuildFirePool.Get().GetComponent<BuildEffect>();
            lastPoolUsed = BuildFirePool;
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            if(playerStateMachine.CurrentState != PlayerStates.BUILD) return;
            CancelInvoke();
            var handPos = player.HandTransform.transform.position;
            var buildSpots = AttractionSpot.AttractionSpots.ToArray();
            var buildRange = player.BuildData.BuildSpotDetectionRange;
            
            var openSpots = RangeTargetScanner.GetTargets(handPos, buildSpots, buildRange).Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
            if(openSpots.Length <= 0) return;
            var targetAttractionSpotPosition = ClosestEntityFinder.GetClosestTransform(openSpots, handPos).position;
            
            SetTargetedPosition(targetAttractionSpotPosition);
            Invoke(nameof(StopEffect), player.BuildData.BuildTime);
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