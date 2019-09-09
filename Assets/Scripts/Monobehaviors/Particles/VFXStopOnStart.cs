using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;


public class VFXStopOnStart : MonoBehaviour
{
    void Awake()
    {
        GetComponent<VisualEffect>().Stop();
    }
}
