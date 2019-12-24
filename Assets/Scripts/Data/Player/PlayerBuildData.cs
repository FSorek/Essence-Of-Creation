using System;
using System.Collections.Generic;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "Player Build Data", menuName = "Essence/Player/Build Data")]
    public class PlayerBuildData : ScriptableObject
    {

        public TransformList AttractionSpots;

        public GameObject AirEssencePrefab;
        public GameObject EarthEssencePrefab;
        public GameObject FireEssencePrefab;
        public GameObject WaterEssencePrefab;
        public GameObject ObeliskAttractionPrefab;
        public GameObject ObeliskBlockPrefab;
        public GameObject ObeliskBasePrefab;
        public GameObject EssenceAttractionPointPrefab;
        public float BuildDistanceOffset;
        
        
        public float BuildSpotDetectionRange = 20f;
        public float BuildTime = 2f;
        public int MaxObeliskSize = 3;
        private List<GameObject> extractedEssences = new List<GameObject>();

        public GameObject CurrentEssence { get; set; }
        public AttractionSpot TargetAttraction { get; set; }
        public Transform ConstructorObject { get; private set; }
        public List<GameObject> ExtractedEssences => extractedEssences;
        private void Awake()
        {
            ConstructorObject = FindObjectOfType<HandHover>().transform;
        }
    }
 }