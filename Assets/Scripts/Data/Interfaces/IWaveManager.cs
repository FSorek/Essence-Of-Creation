using UnityEngine;

public interface IWaveManager
{
    ITakeDamage[] GetEnemiesAlive();
    UnitData GetCurrentGeneratedUnit();
    Transform[] Reachpoints { get; }
}