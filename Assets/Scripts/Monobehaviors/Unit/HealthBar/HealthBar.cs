using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImg;
    [SerializeField] private float updateSpeed = 0.5f;
    [SerializeField] private float positionOffset;
    private Camera mainCamera;

    private ITakeDamage attachedEntity;
    internal void SetHealth(ITakeDamage entity)
    {
        attachedEntity = entity;
        foregroundImg.fillAmount = 1f;
        mainCamera = Camera.main;
        attachedEntity.OnTakeDamage += HandleHealthChanged;
    }

    private void HandleHealthChanged(Damage damage)
    {
        // later on add different visuals depending on the dominating type of damage
        var pct = attachedEntity.CurrentHealth/attachedEntity.MaxHealth;
        StartCoroutine(ResizeToPercentage(pct));
    }

    private IEnumerator ResizeToPercentage(float pct)
    {
        var preChangePercentage = foregroundImg.fillAmount;
        var elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            foregroundImg.fillAmount = Mathf.Lerp(preChangePercentage, pct, elapsed / updateSpeed);
            yield return null;
        }

        foregroundImg.fillAmount = pct;
    }

    private void LateUpdate()
    {
        if(attachedEntity != null && !attachedEntity.Equals(null))
            transform.position = mainCamera.WorldToScreenPoint(attachedEntity.Position + Vector3.up * positionOffset);
    }

    private void OnDisable()
    {
        if(attachedEntity != null)
            attachedEntity.OnTakeDamage -= HandleHealthChanged;
    }
}

