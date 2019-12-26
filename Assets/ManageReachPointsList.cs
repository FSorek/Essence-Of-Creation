using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObjects.Globals;
using UnityEngine;

public class ManageReachPointsList : MonoBehaviour
{
    [SerializeField] private TransformList ReachpointList;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ReachpointList.Add(transform.GetChild(i));
        }
    }
}
