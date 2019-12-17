using System;
using System.Collections;
using Monobehaviors.Unit;
using UnityEngine;

public class ManageGlobalList : MonoBehaviour
{
    public TransformList ListAsset;

    private void OnEnable()
    {
        ListAsset.Add(transform);
    }

    private void OnDisable()
    {
        ListAsset.Remove(transform);
    }
}