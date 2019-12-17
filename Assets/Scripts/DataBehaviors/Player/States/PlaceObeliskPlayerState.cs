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
        private Transform parent;

        public PlaceObeliskPlayerState(PlayerBuildData buildData, PlayerInput input, PlayerStateData stateData) : base(stateData)
        {
            this.buildData = buildData;
            this.input = input;
            buildBlocks = new List<GameObject>();
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            var topBlockPosition = buildBlocks.Last().transform.position;
            var peak = GameObject.Instantiate(buildData.ObeliskAttractionPrefab, topBlockPosition + placePointBuildDirection, Quaternion.LookRotation(placePointBuildDirection), parent);
            peak.AddComponent<AttractionSpot>();
            buildBlocks.Clear();
            parent = null;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnIncreasePressed()
        {
            if (buildBlocks.Count < buildData.MaxObeliskSize + 1) // +1 to not count the initial Base block
            {
                var block = GameObject.Instantiate(buildData.ObeliskBlockPrefab, parent);
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
            var position = buildData.ConstructorObject.position;
            float lastDistance = float.MaxValue;
            Vector3 point = Vector3.zero;
            foreach (var collider in FloorColliderManager.Instance.BuildAreaTransforms)
            {
                var currentPoint = collider.GetComponent<Collider>().ClosestPoint(position);
                float currentDistance = Vector3.Distance(currentPoint, position);
                if (currentDistance < lastDistance)
                {
                    point = currentPoint;
                    lastDistance = currentDistance;
                }
            }
            if (point == Vector3.zero)
                return;
            
            placePointBuildDirection = (position - point).normalized;
            Debug.DrawLine(position, point, Color.blue);
            var distanceIndex = 1;
            foreach (var block in buildBlocks)
            {
                var blockTransform = block.transform;
                blockTransform.LookAt(position);
                blockTransform.position = point + 2f * distanceIndex++ * placePointBuildDirection;
            }
        }

        public override void OnStateExit()
        {
            input.OnIncreasePressed -= PlayerInputOnIncreasePressed;
            input.OnDecreasePressed -= PlayerInputOnDecreasePressed;
            input.OnPrimaryKeyPressed -= PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed -= PlayerInputOnSecondaryKeyPressed;

            if(parent != null)
            {
                buildBlocks.Clear();
                GameObject.Destroy(parent.gameObject);
            }
        }

        public override void OnStateEnter()
        {
            input.OnIncreasePressed += PlayerInputOnIncreasePressed;
            input.OnDecreasePressed += PlayerInputOnDecreasePressed;
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed += PlayerInputOnSecondaryKeyPressed;

            parent = GameObject.Instantiate(new GameObject("Obelisk")).transform;
            var block = GameObject.Instantiate(buildData.ObeliskBasePrefab, parent);
            buildBlocks.Add(block);
        }

        private void PlayerInputOnSecondaryKeyPressed()
        {
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }
    }
}