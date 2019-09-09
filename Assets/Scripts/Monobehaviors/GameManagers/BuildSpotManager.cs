using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildSpotManager : MonoBehaviour
{
    public static List<IEntity> BuildSpots => Instance.buildSpots;
    private static BuildSpotManager Instance;

    private List<IEntity> buildSpots;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("More than one Instance Manager in the instance!");

        Instance = this;
        buildSpots = new List<IEntity>();
        var buildSpotScripts = new List<GameObject>(GameObject.FindGameObjectsWithTag("BuildSpot"));
        foreach (var buildSpotScript in buildSpotScripts)
        {
            buildSpots.Add(buildSpotScript.GetComponent<IEntity>());
        }
    }
}
