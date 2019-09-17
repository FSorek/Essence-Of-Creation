using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : GameEntity
{
    public bool IsOccupied => CurrentTower != null;
    public GameObject CurrentTower { get; private set; }

    private void Awake()
    {
        PlayerBuildManager.OnTowerCreated += AssignTower;
    }

    private void AssignTower(GameObject tower, BuildSpot spot)
    {
        if(spot != transform)
            return;

        CurrentTower = tower;
        PlayerBuildManager.OnTowerCreated -= AssignTower;
    }

    public void SetCurrentTower(GameObject tower)
    {
        CurrentTower = tower;
        PlayerBuildManager.OnTowerCreated -= AssignTower;
    }

    public void ClearCurrentTower()
    {
        CurrentTower = null;
        PlayerBuildManager.OnTowerCreated += AssignTower;
    }
}
