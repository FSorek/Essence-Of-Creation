using System.Collections;
using Data.Game;
using Data.Interfaces.Game.Waves;
using DataBehaviors.Game.Waves;
using Monobehaviors.Pooling;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public Transform ReachpointsParent;
        public int Seed;
        public IWaveSpawner Spawner;
        public Transform SpawnPosition;
        private float timer;
        public ObjectPool UnitPool;
        public WaveSettings WaveSettings;
        public static IWaveManager Instance { get; private set; }

        private void Awake()
        {
            if (Spawner == null || Spawner.Equals(null))
                Spawner = new WaveSpawner(WaveSettings, UnitPool);
            if (Instance == null || Instance.Equals(null))
                Instance = new WaveManagerController(Seed, WaveSettings);
            Instance.SetReachpoints(ReachpointsParent);
            timer = WaveSettings.TimeToFirstWave;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(SpawnWave());
                Instance.NextWave();
                timer = WaveSettings.TimeBetweenWaves;
            }
        }

        private IEnumerator SpawnWave()
        {
            int enemiesPerSpawn = Random.Range(1, 4);
            int numberOfSpawns = WaveSettings.EnemiesPerWave / enemiesPerSpawn;
            float timeBetweenSpawns = WaveSettings.TimeBetweenWaves / numberOfSpawns;
            for (int i = 0; i < numberOfSpawns; i++)
            {
                Spawner.Spawn(SpawnPosition.position, enemiesPerSpawn);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}