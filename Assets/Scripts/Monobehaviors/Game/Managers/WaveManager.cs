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
            StartCoroutine(SpawnWave());
            waveManagerController.NextWave();
            timer = WaveSettings.TimeBetweenWaves;
        }
    }

    private IEnumerator SpawnWave()
    {
        int enemiesPerSpawn = Random.Range(1,4);
        int numberOfSpawns = WaveSettings.EnemiesPerWave / enemiesPerSpawn;
        float timeBetweenSpawns = (float)WaveSettings.TimeBetweenWaves / (float)numberOfSpawns;
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Spawner.Spawn(SpawnPosition.position, enemiesPerSpawn);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        
    }
}
