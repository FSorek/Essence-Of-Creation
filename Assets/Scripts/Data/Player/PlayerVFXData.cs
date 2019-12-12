using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "Player VFX Data", menuName = "Essence/Player/VFX Data", order = 1)]
    public class PlayerVFXData : ScriptableObject
    {
        public GameObject SummonFire;
        public GameObject SummonAir;
        public GameObject SummonEarth;
        public GameObject SummonWater;
    }
}