using System.Collections.Generic;
using Data.Data_Types;
using UnityEngine;

namespace Monobehaviors.Tower
{
    public class Obelisk : MonoBehaviour
    {
        public static List<Obelisk> ObeliskEntities = new List<Obelisk>();
        public List<BaseElement> InfusedElements = new List<BaseElement>();
        public Vector3 AttackSpawnPosition => transform.position;

        private void Awake()
        {
            ObeliskEntities.Add(this);
        }

        private void OnDestroy()
        {
            ObeliskEntities.Remove(this);
        }
    }
}