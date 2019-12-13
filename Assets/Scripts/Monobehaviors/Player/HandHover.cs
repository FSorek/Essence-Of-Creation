using System;
using System.Collections;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Utility;
using DataBehaviors.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Camera;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class HandHover : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerBuildData buildData;
        [SerializeField] private PlayerStateData stateData;
        
        [SerializeField] private float heightAboveSurface = 2f;
        [SerializeField] private bool isCursorVisible;
        [SerializeField] private float updateSpeed;
        [SerializeField] private float heightAdjustmentScroll;

        private float heightAdjustment;
        private int pressCounter;

        private void Awake()
        {
            input.OnIncreasePressed += PlayerInputOnIncreasePressed;
            input.OnDecreasePressed += PlayerInputOnDecreasePressed;
        }

        private void PlayerInputOnDecreasePressed()
        {
            if(stateData.CurrentState != PlayerStates.PLACE_OBELISK
               || pressCounter <= 0)
                return;
            pressCounter--;
            SetHoverHeight(heightAdjustment - heightAdjustmentScroll);
        }

        private void PlayerInputOnIncreasePressed()
        {
            if(stateData.CurrentState != PlayerStates.PLACE_OBELISK
               || pressCounter >= buildData.MaxObeliskSize)
                return;
            pressCounter++;
            SetHoverHeight(heightAdjustment + heightAdjustmentScroll);
        }

        private void SetHoverHeight(float height)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothMove(height));
        }
        
        private IEnumerator SmoothMove(float height)
        {
            float elapsed = 0f;
            float initialAdjustment = heightAdjustment;

            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                heightAdjustment = Mathf.Lerp(initialAdjustment, height, elapsed / updateSpeed);
                yield return null;
            }

            heightAdjustment = height;
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