﻿using UnityEngine;

public interface IWaveManager
{
    int CurrentWave { get; }
    ITakeDamage[] EnemiesAlive { get; }
    UnitData CurrentGeneratedUnit { get; }
    Transform[] Reachpoints { get; }

    void SetReachpoints(Transform reachpointsParent);
    void NextWave();
}