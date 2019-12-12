using System;
using DataBehaviors.Player.States;
using Monobehaviors.Camera;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class HandHover : MonoBehaviour
    {
        [SerializeField]private float heightAboveSurface = 2f;
        [SerializeField]private bool isCursorVisible;
        [SerializeField] private float speed;

        private float heightAdjustment = 0;

        private void Awake()
        {
            PlacingBuildSpotPlayerState.OnIncreaseBuildSpotHeight += PlacingBuildSpotPlayerStateOnIncreaseBuildSpotHeight;
            PlacingBuildSpotPlayerState.OnDecreaseBuildSpotHeight += PlacingBuildSpotPlayerStateOnDecreaseBuildSpotHeight;
            PlacingBuildSpotPlayerState.OnBuildSpotCancelled += ResetHeightAdjustment;
            PlacingBuildSpotPlayerState.OnBuildSpotCreated += ResetHeightAdjustment;
        }

        private void ResetHeightAdjustment()
        {
            heightAdjustment = 0;
        }

        private void PlacingBuildSpotPlayerStateOnDecreaseBuildSpotHeight()
        {
            if(heightAdjustment > 0)
                heightAdjustment--;
        }

        private void PlacingBuildSpotPlayerStateOnIncreaseBuildSpotHeight()
        {
            if(heightAdjustment < 3)
                heightAdjustment++;
        }

        private void FixedUpdate()
        {
            var hit = MouseWorldPoint.RaycastHit;
            if (hit.HasValue)
            {
                transform.position = hit.Value.point + new Vector3(0, heightAboveSurface + heightAdjustment, -heightAboveSurface-heightAdjustment);
                Cursor.visible = isCursorVisible;
            }
        }
    }
}