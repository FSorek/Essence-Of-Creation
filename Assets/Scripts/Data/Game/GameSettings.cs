using System;
using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Essence/Game/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public int CurrentWave;
        public int[] WavesPowerPoints;
        public float TimeToFirstWave;
        public float TimeBetweenWaves;
        public int WaveCount => WavesPowerPoints.Length;
    }
}