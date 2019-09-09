using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandHover : MonoBehaviour
{
    public float HeightAboveSurface = 2f;
    public bool IsCursorVisible = false;
    void FixedUpdate()
    {
        var hit = MouseWorldPoint.RaycastHit;
        if (hit.HasValue)
        {
            transform.position = hit.Value.point + new Vector3(0, HeightAboveSurface, -HeightAboveSurface);
            Cursor.visible = IsCursorVisible;
        }
    }
}
