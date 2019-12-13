using System;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "Player Build Data", menuName = "Essence/Player/Build Data")]
    public class PlayerBuildData : ScriptableObject
    {
        public GameObject CurrentEssence { get; set; }
        public AttractionSpot TargetAttraction { get; set; }
        public Transform ConstructorObject;
        
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

        private void OnEnable()
        {
            ConstructorObject = FindObjectOfType<HandHover>().transform;
        }
    }
 }