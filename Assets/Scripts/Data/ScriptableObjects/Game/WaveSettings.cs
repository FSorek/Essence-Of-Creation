using Data.ScriptableObjects.Waves;
using UnityEngine;

namespace Data.ScriptableObjects.Game
{
    [CreateAssetMenu(fileName = "Wave Settings", menuName = "Essence/Game/Wave Settings")]
    public class WaveSettings : ScriptableObject
    {
        [SerializeField] private int currentWave;
        
        public int Seed;
        public float TimeBetweenSpawns;
        public int SpawnAmount;
        public Vector3 SpawnPosition;
        public int EnemiesPerSpawn;

        public StatConversionRates ConversionRates;
        public int[] EnemyPower;

        public int WaveCount => EnemyPower.Length;
        public int CurrentWave => currentWave;

        public void NextWave() => currentWave++;
    }
}