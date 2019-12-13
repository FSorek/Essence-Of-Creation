using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "Player Building Data", menuName = "Essence/Player/Building Data", order = 0)]
    public class PlayerBuildData : ScriptableObject
    {
        public GameObject AirEssencePrefab;
        public GameObject EarthEssencePrefab;
        public GameObject FireEssencePrefab;
        public GameObject WaterEssencePrefab;
        public GameObject ObeliskAttractionPrefab;
        public GameObject ObeliskBlockPrefab;
        public GameObject ObeliskBasePrefab;
        
        
        public float BuildSpotDetectionRange = 20f;
        public float BuildTime = 2f;
        public int MaxObeliskSize = 3;
    }
 }