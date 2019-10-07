using UnityEngine;

public interface IWaveSpawner
{
    void Spawn(Vector3 position, int amount = 1);
}
