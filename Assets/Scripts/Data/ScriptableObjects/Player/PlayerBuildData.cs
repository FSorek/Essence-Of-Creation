using System;
using System.Collections.Generic;
using Data.ScriptableObjects.Globals;
using Monobehaviors.AttractionSpots;
using Monobehaviors.Players;
using UnityEngine;

namespace Data.ScriptableObjects.Player
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
        [SerializeField]private List<GameObject> extractedEssences;
        private Transform constructorObject;

        public GameObject CurrentEssence { get; set; }
        public AttractionSpot TargetAttraction { get; set; }
        public Transform ConstructorObject
        {
            get 
            {            
                if (constructorObject == null)
                    constructorObject = FindObjectOfType<HandHover>().transform;
                return constructorObject; 
            }
        }

        public List<GameObject> ExtractedEssences        
        {
            get 
            {            
                if (extractedEssences == null)
                    extractedEssences = new List<GameObject>();
                return extractedEssences; 
            }
        }

        private void OnEnable()
        {
            extractedEssences = null;
        }
    }
 }