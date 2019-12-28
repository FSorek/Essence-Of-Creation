using System;
using System.Collections.Generic;
using Data.Data_Types.Enums;
using DataBehaviors.Recipes;
using UnityEngine;

namespace Monobehaviors.Essences
{
    public class Essence : MonoBehaviour
    {
        [SerializeField]private List<BaseElement> InfusedElements = new List<BaseElement>();
        public int EssenceId => TowerRecipeId.GetID(InfusedElements);
        public int ElementCount => InfusedElements.Count;
    }
}