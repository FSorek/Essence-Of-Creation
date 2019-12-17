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
        [SerializeField]private UnityEngine.Camera playerCamera;
        
        [SerializeField] private float heightAboveSurface = 2f;
        [SerializeField] private bool isCursorVisible;
        [SerializeField] private float updateSpeed;
        [SerializeField] private float heightAdjustmentScroll;

        [SerializeField] private float cameraViewMargin;
        [SerializeField] private float handSpeed = 5f;

        private float heightAdjustment;
        private int pressCounter;
        private Vector3 screenPosition;
        private Transform handTransform;

        private void Awake()
        {
            input.OnIncreasePressed += PlayerInputOnIncreasePressed;
            input.OnDecreasePressed += PlayerInputOnDecreasePressed;
            handTransform = transform;
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
            var transformPosition = handTransform.position;
            screenPosition = playerCamera.WorldToViewportPoint(transformPosition);
            
            if((screenPosition.x < 1 - cameraViewMargin && input.Horizontal > 0) || (screenPosition.x > 0 + cameraViewMargin && input.Horizontal < 0))
                MoveHand(Vector3.right * input.Horizontal);
            if((screenPosition.y < 1 - cameraViewMargin && input.Vertical > 0) || (screenPosition.y > 0 + cameraViewMargin && input.Vertical < 0))
                MoveHand(Vector3.forward * input.Vertical);

            if ((screenPosition.x > 1 - cameraViewMargin && input.Vertical < 0))
            {
                MoveHand(Vector3.right * input.Vertical);
            }
            if (screenPosition.x < 0 + cameraViewMargin && input.Vertical < 0)
            {
                MoveHand(Vector3.left * input.Vertical);
            }

            transformPosition = handTransform.position;
            transformPosition = new Vector3(transformPosition.x, heightAboveSurface + heightAdjustment, transformPosition.z);
            handTransform.position = transformPosition;
        }

        private void MoveHand(Vector3 direction)
        {
            handTransform.Translate(handSpeed * Time.deltaTime * direction);
        }
    }
}