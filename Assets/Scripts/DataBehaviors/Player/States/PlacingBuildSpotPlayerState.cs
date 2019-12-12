using System;
using DataBehaviors.Game.Entity.Targeting;
using DataBehaviors.Game.Utility;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class PlacingBuildSpotPlayerState : PlayerState
    {
        private readonly Transform[] buildSpotFloorColliders;
        private Transform closestAvailableSpace;

        private float lastColliderScanTime;

        public PlacingBuildSpotPlayerState(PlayerComponent playerC) : base(playerC)
        {
            buildSpotFloorColliders = FloorColliderManager.Instance.BuildColliders;
            closestAvailableSpace =
                ClosestEntityFinder.GetClosestTransform(buildSpotFloorColliders,
                    playerC.HandTransform.position);
        }

        public static event Action OnIncreaseBuildSpotHeight;
        public static event Action OnDecreaseBuildSpotHeight;
        public static event Action OnBuildSpotCreated;
        public static event Action OnBuildSpotCancelled;

        public override void ListenToState()
        {
            if (closestAvailableSpace == null)
                return;
            var closestPoint = closestAvailableSpace.GetComponent<Collider>()
                .ClosestPointOnBounds(playerComponentC.HandTransform.position);
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput > 0f)
                OnIncreaseBuildSpotHeight();
            if (wheelInput < 0f)
                OnDecreaseBuildSpotHeight();
            if (Input.GetMouseButtonDown(0))
                OnBuildSpotCreated();
            else if (Input.GetMouseButtonDown(1)) OnBuildSpotCancelled();
        }

        public override void OnStateExit()
        {
            OnBuildSpotCancelled();
        }
    }
}