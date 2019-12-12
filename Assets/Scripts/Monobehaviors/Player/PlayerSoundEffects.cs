using UnityEngine;

namespace Monobehaviors.Player
{
    [CreateAssetMenu(fileName = "Player SFX", menuName = "Essence/Player/Sound Effects", order = 1)]
    public class PlayerSoundEffects : ScriptableObject
    {
        public AudioClip Summoning;
        public AudioClip SummonFireStart;
    }
}