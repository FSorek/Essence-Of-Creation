using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "Player Building Data", menuName = "Essence/Player/Building Data", order = 0)]
    public class PlayerBuildingData : ScriptableObject
    {
        public float BuildSpotDetectionRange = 20f;

        public GameObject BuildSpotFundamentals;
        public float BuildTime = 2f;
    }
}