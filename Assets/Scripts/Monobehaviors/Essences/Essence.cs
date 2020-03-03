using System;
using System.Collections.Generic;
using Data.Data_Types.Enums;
using DataBehaviors.Recipes;
using Monobehaviors.Essences.Attacks;
using UnityEngine;

namespace Monobehaviors.Essences
{
    public class Essence : MonoBehaviour
    {
        [SerializeField]private List<BaseElement> InfusedElements = new List<BaseElement>();
        private Attack[] attacks;
        private float deactivateScale = .5f;
        public int EssenceId => EssenceIdGenerator.GetID(InfusedElements);
        public int ElementCount => InfusedElements.Count;

        private void Awake()
        {
            attacks = GetComponents<Attack>();
        }

        public void Deactivate()
        {
            foreach (var attack in attacks)
            {
                attack.enabled = false;
            }
            transform.localScale = new Vector3(deactivateScale, deactivateScale, deactivateScale);
        }

        public void Activate()
        {
            foreach (var attack in attacks)
            {
                attack.enabled = true;
            }
            transform.localScale = Vector3.one;
        }
    }
}