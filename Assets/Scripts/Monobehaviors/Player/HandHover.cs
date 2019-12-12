using Monobehaviors.Camera;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class HandHover : MonoBehaviour
    {
        public float HeightAboveSurface = 2f;
        public bool IsCursorVisible;

        private void FixedUpdate()
        {
            var hit = MouseWorldPoint.RaycastHit;
            if (hit.HasValue)
            {
                transform.position = hit.Value.point + new Vector3(0, HeightAboveSurface, -HeightAboveSurface);
                Cursor.visible = IsCursorVisible;
            }
        }
    }
}