using System;
using System.Collections;
using Data.Data_Types;
using DataBehaviors.Game.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Monobehaviors.Unit.HealthBar
{
    internal class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image foregroundImg;
        [SerializeField] private float positionOffset;
        [SerializeField] private float updateSpeed = 0.5f;
        private UnityEngine.Camera mainCamera;
        private UnitHealth attachedHealth;

        private void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
        }

        internal void SetHealth(UnitHealth entity)
        {
            attachedHealth = entity;
            if (attachedHealth != null)
                attachedHealth.OnTakeDamage -= HandleHealthChanged;
            foregroundImg.fillAmount = 1f;
            attachedHealth.OnTakeDamage += HandleHealthChanged;
        }

        private void HandleHealthChanged(Damage damage)
        {
            // later on add different visuals depending on the dominating type of damage
            if(!gameObject.activeSelf) return;
            float pct = attachedHealth.CurrentHealth / (float) attachedHealth.MaxHealth;
            StopAllCoroutines();
            StartCoroutine(ResizeToPercentage(pct));
        }

        private IEnumerator ResizeToPercentage(float pct)
        {
            float preChangePercentage = foregroundImg.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateSpeed)
            {
                elapsed += GameTime.deltaTime;
                foregroundImg.fillAmount = Mathf.Lerp(preChangePercentage, pct, elapsed / updateSpeed);
                yield return null;
            }

            foregroundImg.fillAmount = pct;
        }

        private void LateUpdate()
        {
            if (attachedHealth != null)
                transform.position = mainCamera.WorldToScreenPoint(attachedHealth.transform.position + Vector3.up * positionOffset);
        }
    }
}