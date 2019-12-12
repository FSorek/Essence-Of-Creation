using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Wave Settings", menuName = "Essence/Game/Wave Settings", order = 0)]
    public class WaveSettings : ScriptableObject
    {
        public int EnemiesPerWave = 60;
        public GameObject[] ModelPool;
        public float TimeBetweenWaves = 20;
        public float TimeToFirstWave;
        public int[] WavesPowerPoints; // Random model selection for enemies
        public int WaveCount => WavesPowerPoints.Length;
    }
}