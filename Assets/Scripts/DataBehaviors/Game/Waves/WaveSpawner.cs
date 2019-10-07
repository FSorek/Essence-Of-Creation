using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : IWaveSpawner
{
    private readonly WaveSettings settings;
    private readonly ObjectPool currentPool;

    public WaveSpawner(WaveSettings settings, ObjectPool currentPool)
    {
        this.settings = settings;
        this.currentPool = currentPool;
    }

    public void Spawn(Vector3 position, int amount = 1)
    {
        //var model = settings.ModelPool[Random.Range(0, settings.ModelPool.Length)];
        for (int i = 0; i < amount; i++)
        {
            var unitEntity = currentPool.Get();
            unitEntity.transform.position = position;
            unitEntity.gameObject.SetActive(true);
        }
    }
}
