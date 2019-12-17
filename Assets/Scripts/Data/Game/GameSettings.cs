using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Essence/Game/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int Seed;
        public int WaveCount;
        public int[] WavesPowerPoints;
        public float TimeToFirstWave;
        public float TimeBetweenWaves;
    }
}