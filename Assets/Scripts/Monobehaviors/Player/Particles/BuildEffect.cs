using Data.Extensions;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

namespace Monobehaviors.Player.Particles
{
    public class BuildEffect : MonoBehaviour
    {
        internal void SetFollowedTransform(Transform effectPositionTransform, Vector3 buildSpotPosition)
        {
            var binders = GetComponents<VFXPositionBinder>();
            foreach (var vfxPositionBinder in binders)
                if (vfxPositionBinder.Parameter == "HandPosition" && vfxPositionBinder.Target == null)
                    vfxPositionBinder.Target = effectPositionTransform;
            transform.GetChild(0).position = buildSpotPosition;
            gameObject.SetActive(true);
            GetComponent<VisualEffect>().SendEvent("OnPlay");
        }

        internal void Detach()
        {
            GetComponent<VisualEffect>().SendEvent("OnStop");
            if(gameObject.activeSelf)
                StartCoroutine(gameObject.DelayedReturnToPool(6f));
        }
    }
}