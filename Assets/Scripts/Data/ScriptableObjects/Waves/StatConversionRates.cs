using UnityEngine;

namespace Data.ScriptableObjects.Waves
{
    [CreateAssetMenu(fileName = "Conversion Rates", menuName = "Essence/Game/Stat Conversion Rates")]

    public class StatConversionRates : ScriptableObject
    {
        public Vector2Int EffectiveHealthRange;
        public Vector2Int ArmorToughnessRange;
        public Vector2 MovementSpeedRange;
        public Vector2 HealthRegenRange;
        
        public float EffectiveHealthRatio = .5f;
        public float ArmorToughnessRatio = 1f;
        [Range(0f, .9f)]
        public float MovementSpeedMaxBonus = .5f;
        public float HealthRegenRatio = 3f;        
    }
}