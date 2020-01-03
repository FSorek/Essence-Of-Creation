using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public abstract class HitAbility : MonoBehaviour
    {
        public abstract void ApplyAbility(Transform target);
    }
}