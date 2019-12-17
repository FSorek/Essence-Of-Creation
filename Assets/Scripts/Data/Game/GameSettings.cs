using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Essence/Game/Game Settings", order = 0)]
    public class Game : ScriptableObject
    {
        public int Seed;
        public int WaveCount;
        public int[] WavesPowerPoints;
    }
}