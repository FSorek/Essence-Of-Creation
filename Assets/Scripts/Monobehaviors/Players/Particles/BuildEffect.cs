using System;
using Data.Extensions;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Monobehaviors.Players.Particles
{
    public class BuildEffect : MonoBehaviour
    {
        private readonly int propertyId = Shader.PropertyToID("HandPosition");
        private Transform effectPositionTransform;
        private VisualEffect effect;

        internal void SetFollowedTransform(Transform effectPositionTransform, Vector3 buildSpotPosition)
        {
            this.effectPositionTransform = effectPositionTransform;
            transform.GetChild(0).position = buildSpotPosition;
            gameObject.SetActive(true);
            effect = GetComponent<VisualEffect>();
            effect.SendEvent("OnPlay");
        }

        private void Update()
        {
            if(effect != null)
                effect.SetVector3(propertyId, effectPositionTransform.position);
        }

        internal void Detach()
        {
            GetComponent<VisualEffect>().SendEvent("OnStop");
            if(gameObject.activeSelf)
                StartCoroutine(gameObject.DelayedReturnToPool(6f));
        }
    }
}