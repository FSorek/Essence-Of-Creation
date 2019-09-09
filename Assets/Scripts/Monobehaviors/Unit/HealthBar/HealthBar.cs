using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImg;
    [SerializeField] private float updateSpeed = 0.5f;
    [SerializeField] private float positionOffset;

    private Unit unitAttached;
    internal void SetHealth(Unit unit)
    {
        unitAttached = unit;
        unit.OnTakeDamage += HandleHealthChanged;
        foregroundImg.fillAmount = 1f;
    }

    private void HandleHealthChanged(Damage damage)
    {
        // later on add different visuals depending on the dominating type of damage
        var pct = unitAttached.CurrentHealth/unitAttached.MaxHealth;
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
        if(unitAttached != null)
            transform.position =
                Camera.main.WorldToScreenPoint(unitAttached.transform.position + Vector3.up * positionOffset);
    }

    private void OnDisable()
    {
        if(unitAttached != null)
            unitAttached.OnTakeDamage -= HandleHealthChanged;
    }
}