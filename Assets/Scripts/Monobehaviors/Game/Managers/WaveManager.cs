using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public ObjectPool UnitPool;
    public WaveSettings WaveSettings;
    public int Seed;
    public static IWaveManager Instance => waveManagerController;
    public IWaveSpawner Spawner;
    public Transform SpawnPosition;
    public Transform ReachpointsParent;

    private static IWaveManager waveManagerController;
    private float timer;

    private void Awake()
    {
        if(Spawner == null || Spawner.Equals(null))
            Spawner = new WaveSpawner(WaveSettings, UnitPool);
        if(waveManagerController == null || waveManagerController.Equals(null))
            waveManagerController = new WaveManagerController(Seed, WaveSettings);
        waveManagerController.SetReachpoints(ReachpointsParent);
        timer = WaveSettings.TimeToFirstWave;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawner.Spawn(SpawnPosition.position);
            waveManagerController.NextWave();
            timer = WaveSettings.TimeBetweenWaves;
        }
    }
}
