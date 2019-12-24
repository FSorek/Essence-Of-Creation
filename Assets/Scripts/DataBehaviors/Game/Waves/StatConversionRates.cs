using UnityEngine;

namespace DataBehaviors.Game.Waves
{
    [CreateAssetMenu(fileName = "Conversion Rates", menuName = "Essence/Game/Stat Conversion Rates")]

    public class StatConversionRates : ScriptableObject
    {
        public Vector2 MovementSpeedRange;
        public Vector2Int EffectiveHealthRange;
        public Vector2 HealthRegenRange;
        
        public float MovementSpeedRatio = 2f;
        public float EffectiveHealthRatio = .5f;
        public float HealthRegenRatio = 3f;
    }
}