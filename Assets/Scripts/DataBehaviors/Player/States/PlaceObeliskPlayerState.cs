using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
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
        private readonly PlayerBuildData buildData;
        private readonly PlayerInput input;
        private readonly List<GameObject> buildBlocks;
        private Vector3 placePointBuildDirection;

        public PlaceObeliskPlayerState(PlayerBuildData buildData, PlayerInput input, PlayerStateData stateData) : base(stateData)
        {
            this.buildData = buildData;
            this.input = input;
            buildBlocks = new List<GameObject>();
        }


        private void PlayerInputOnPrimaryKeyPressed()
        {
            var topBlockPosition = buildBlocks.Last().transform.position;
            var peak = GameObject.Instantiate(buildData.ObeliskAttractionPrefab, topBlockPosition + placePointBuildDirection, Quaternion.LookRotation(placePointBuildDirection));
            peak.AddComponent<AttractionSpot>();
            buildBlocks.Clear();
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnIncreasePressed()
        {
            if (buildBlocks.Count < buildData.MaxObeliskSize + 1) // +1 to not count the initial Base block
            {
                var block = GameObject.Instantiate(buildData.ObeliskBlockPrefab);
                var scale = block.transform.lossyScale.x * Mathf.Pow(.8f, buildBlocks.Count);
                block.transform.localScale = new Vector3(scale,scale, scale);
                buildBlocks.Add(block);
            }
        }
        private void PlayerInputOnDecreasePressed()
        {
            if (buildBlocks.Count > 1)
            {
                var block = buildBlocks.Last();
                buildBlocks.Remove(block);
                GameObject.Destroy(block);
            }
        }

        public override void ListenToState()
        {
            var closestBuildTransform = ClosestEntityFinder.GetClosestTransform(FloorColliderManager.Instance.BuildAreaTransforms, buildData.ConstructorObject.position);
            if (closestBuildTransform == null)
                return;

            var position = buildData.ConstructorObject.position;
            var obeliskPlacePoint = closestBuildTransform.GetComponent<Collider>().ClosestPointOnBounds(position);
            placePointBuildDirection = (position - obeliskPlacePoint).normalized;
            Debug.DrawRay(position, placePointBuildDirection * 10f, Color.blue);
            var distanceIndex = 1;
            foreach (var block in buildBlocks)
            {
                var blockTransform = block.transform;
                blockTransform.LookAt(position);
                blockTransform.position = obeliskPlacePoint + 2f * distanceIndex++ * placePointBuildDirection;
            }
        }

        public override void OnStateExit()
        {
            input.OnIncreasePressed -= PlayerInputOnIncreasePressed;
            input.OnDecreasePressed -= PlayerInputOnDecreasePressed;
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            for (int i = 0; i < buildBlocks.Count; i++)
            {
                var block = buildBlocks.Last();
                buildBlocks.Remove(block);
                GameObject.Destroy(block);
            }
        }

        public override void OnStateEnter()
        {
            input.OnIncreasePressed += PlayerInputOnIncreasePressed;
            input.OnDecreasePressed += PlayerInputOnDecreasePressed;
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            
            var block = GameObject.Instantiate(buildData.ObeliskBasePrefab);
            buildBlocks.Add(block);
        }
    }
}