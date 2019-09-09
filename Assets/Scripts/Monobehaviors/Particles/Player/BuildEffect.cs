using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

public class BuildEffect : MonoBehaviour
{
    private Transform effectPositionTransform;
    internal void SetFollowedTransform(Transform effectPositionTransform, Vector3 buildSpotPosition)
    {
        this.effectPositionTransform = effectPositionTransform;

        var binders = GetComponents<VFXPositionBinder>();
        foreach (var vfxPositionBinder in binders)
        {
            if (vfxPositionBinder.Parameter == "HandPosition" && vfxPositionBinder.Target == null)
                vfxPositionBinder.Target = effectPositionTransform;
        }
        transform.GetChild(0).position = buildSpotPosition;
        gameObject.SetActive(true);
        GetComponent<VisualEffect>().SendEvent("OnPlay");
    }

    internal void Detach()
    {
        GetComponent<VisualEffect>().SendEvent("OnStop");
        gameObject.ReturnToPool(10f);
    }
}

