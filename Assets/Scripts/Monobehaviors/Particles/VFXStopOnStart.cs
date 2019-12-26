using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Monobehaviors.Particles
{
    public class VFXStopOnStart : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<VisualEffect>().Stop();
        }
    }
}