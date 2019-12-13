using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using DataBehaviors.Game.Utility;
using Monobehaviors.BuildSpot;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    /// to-do:
    /// Pooling 
    public class PlaceObeliskPlayerState : PlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        private PlayerBuildData playerBuildData;
        private Stack<GameObject> buildBlocks;
        private Vector3 placePointBuildDirection;

        public PlaceObeliskPlayerState(PlayerComponent player, PlayerStateMachine stateMachine) : base(player)
        {
            this.stateMachine = stateMachine;
            playerBuildData = player.BuildData;
            buildBlocks = new Stack<GameObject>();
        }


        private void PlayerInputOnPrimaryKeyPressed()
        {
            var topBlockPosition = buildBlocks.Peek().transform.position;
            var peak = GameObject.Instantiate(playerBuildData.ObeliskAttractionPrefab, topBlockPosition + placePointBuildDirection, Quaternion.LookRotation(placePointBuildDirection));
            peak.AddComponent<AttractionSpot>();
            buildBlocks.Clear();
            stateMachine.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnIncreasePressed()
        {
            if (buildBlocks.Count < playerBuildData.MaxObeliskSize)
            {
                var block = GameObject.Instantiate(playerBuildData.ObeliskBlockPrefab);
                var scale = block.transform.lossyScale.x * Mathf.Pow(.8f, buildBlocks.Count);
                block.transform.localScale = new Vector3(scale,scale, scale);
                buildBlocks.Push(block);
            }
        }
        private void PlayerInputOnDecreasePressed()
        {
            if (buildBlocks.Count > 0)
            {
                var block = buildBlocks.Pop();
                GameObject.Destroy(block);
            }
        }

        public override void ListenToState()
        {
            var closestBuildTransform = ClosestEntityFinder.GetClosestTransform(FloorColliderManager.Instance.BuildAreaTransforms, player.HandTransform.position);
            if (closestBuildTransform == null)
                return;

            var position = player.HandTransform.position;
            var obeliskPlacePoint = closestBuildTransform.GetComponent<Collider>().ClosestPointOnBounds(position);
            placePointBuildDirection = (position - obeliskPlacePoint).normalized;
            var distanceIndex = 0;
            foreach (var block in buildBlocks)
            {
                var blockTransform = block.transform;
                blockTransform.LookAt(position);
                blockTransform.position = obeliskPlacePoint + ++distanceIndex * 3 * placePointBuildDirection;
            }
        }

        public override void OnStateExit()
        {
            player.PlayerInput.OnIncreasePressed -= PlayerInputOnIncreasePressed;
            player.PlayerInput.OnDecreasePressed -= PlayerInputOnDecreasePressed;
            player.PlayerInput.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            for (int i = 0; i < buildBlocks.Count; i++)
            {
                GameObject.Destroy(buildBlocks.Pop());
            }
        }

        public override void OnStateEnter()
        {
            player.PlayerInput.OnIncreasePressed += PlayerInputOnIncreasePressed;
            player.PlayerInput.OnDecreasePressed += PlayerInputOnDecreasePressed;
            player.PlayerInput.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            
            var block = GameObject.Instantiate(playerBuildData.ObeliskBasePrefab);
            buildBlocks.Push(block);
        }
    }
}