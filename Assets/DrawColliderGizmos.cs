using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColliderGizmos : MonoBehaviour
{
    private Collider[] children;
    private void OnDrawGizmos()
    {
        
    }

    private void OnValidate()
    {
        children = GetComponentsInChildren<Collider>();
    }
}
