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
    public class PlaceObeliskPlayerState : IState
    {
        private readonly PlayerStateData stateData;
        private readonly PlayerBuildData buildData;
        private readonly PlayerInput input;
        private readonly List<GameObject> buildBlocks;
        private Vector3 placePointBuildDirection;
        private Transform parent;

        public PlaceObeliskPlayerState(PlayerBuildData buildData, PlayerInput input, PlayerStateData stateData)
        {
            this.buildData = buildData;
            this.input = input;
            this.stateData = stateData;
            buildBlocks = new List<GameObject>();
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            var topBlockPosition = buildBlocks.Last().transform.position;
            var peak = GameObject.Instantiate(buildData.ObeliskAttractionPrefab, topBlockPosition + placePointBuildDirection * buildData.BuildDistanceOffset, Quaternion.LookRotation(placePointBuildDirection), parent);
            GameObject.Instantiate(new GameObject("Essence"), peak.transform.position + placePointBuildDirection * 2, Quaternion.LookRotation(placePointBuildDirection), parent).AddComponent<AttractionSpot>();
            buildBlocks.Clear();
            parent = null;
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        private void PlayerInputOnIncreasePressed()
        {
            if (buildBlocks.Count < buildData.MaxObeliskSize + 1) // +1 to not count the initial Base block
            {
                var block = GameObject.Instantiate(buildData.ObeliskBlockPrefab, parent.position + placePointBuildDirection * buildBlocks.Count * buildData.BuildDistanceOffset, Quaternion.identity, parent);
                var scale = block.transform.lossyScale.x * Mathf.Pow(.8f, buildBlocks.Count);
                block.transform.localScale = new Vector3(scale,scale, scale);
                block.transform.Rotate(block.transform.forward, Random.Range(30,270));
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

        public void ListenToState()
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
            if (parent != null)
            {
                parent.LookAt(position);
                parent.position = point + placePointBuildDirection * buildData.BuildDistanceOffset;
            }

        }

        public void StateExit()
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

        public void StateEnter()
        {
            input.OnIncreasePressed += PlayerInputOnIncreasePressed;
            input.OnDecreasePressed += PlayerInputOnDecreasePressed;
            input.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            input.OnSecondaryKeyPressed += PlayerInputOnSecondaryKeyPressed;

            parent = GameObject.Instantiate(new GameObject("Obelisk")).transform;
            var block = GameObject.Instantiate(buildData.ObeliskBasePrefab, parent.transform.position, Quaternion.identity, parent);
            buildBlocks.Add(block);
        }

        private void PlayerInputOnSecondaryKeyPressed()
        {
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }
    }
}