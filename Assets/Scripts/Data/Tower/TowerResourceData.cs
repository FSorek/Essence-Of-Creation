using UnityEngine;

namespace Data.Tower
{
    [CreateAssetMenu(fileName = "Entity Resource Data", menuName = "Essence/Entity/Resource Data", order = 1)]
    public class TowerResourceData : ScriptableObject
    {
        public int BuildCost;
        public int RefundAmount;
        public float TimeToBuild;
    }
}