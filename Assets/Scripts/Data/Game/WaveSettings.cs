using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Wave Settings", menuName = "Essence/Game/Wave Settings")]
    public class WaveSettings : ScriptableObject
    {
        public int Seed;
        public float TimeBetweenSpawns;
        public int SpawnAmount;
        public Vector3 SpawnPosition;
        public int EnemiesPerSpawn;
        
        // temporary defined stats
        public int hp;
        public float moveSpeed;
        public int toughness;
        public float hpRegen;
        public int crystal;
    }
}