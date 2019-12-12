using System.Collections;
using Data.Data_Types;
using DataBehaviors.Game.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Monobehaviors.Unit.HealthBar
{
    internal class HealthBar : MonoBehaviour
    {
        private UnitComponent attachedUnit;
        private UnitHealth attachedHealth;
        [SerializeField] private Image foregroundImg;
        private UnityEngine.Camera mainCamera;
        [SerializeField] private float positionOffset;
        [SerializeField] private float updateSpeed = 0.5f;

        internal void SetHealth(UnitComponent entity)
        {
            if (attachedUnit != null)
                attachedUnit.OnTakeDamage -= HandleHealthChanged;
            attachedUnit = entity;
            attachedHealth = attachedUnit.GetComponent<UnitHealth>();
            foregroundImg.fillAmount = 1f;
            mainCamera = UnityEngine.Camera.main;
            attachedUnit.OnTakeDamage += HandleHealthChanged;
        }

        private void HandleHealthChanged(Damage damage)
        {
            // later on add different visuals depending on the dominating type of damage
            if(!gameObject.activeSelf) return;
            float pct = attachedHealth.CurrentHealth / (float) attachedUnit.GetStat(StatName.MaxHealth);
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
            if (attachedUnit != null)
                transform.position = mainCamera.WorldToScreenPoint(attachedUnit.transform.position + Vector3.up * positionOffset);
        }
    }
}