﻿using System;
 using System.Collections.Generic;
using Data.Data_Types;
using Data.Data_Types.Enums;
using DataBehaviors.Recipes;
using Monobehaviors.Essences;
using UnityEngine;

namespace DataBehaviors.Game.Systems
{
    [CreateAssetMenu(fileName = "Recipe Data")]
    public class RecipeData : ScriptableObject
    {
        [SerializeField]private List<Essence> essences = new List<Essence>();
        private Dictionary<int, Essence> recipes = new Dictionary<int, Essence>();


        private void OnEnable()
        {
            recipes = new Dictionary<int, Essence>();
            Debug.Log(recipes.Count);
            foreach (var essence in essences)
            {
                recipes.Add(essence.EssenceId, essence);
                Debug.Log(Convert.ToString(essence.EssenceId,2).PadLeft(16,'0'));
            }
        }

        public Essence TryMerge(Essence essence1, Essence essence2)
        {
            int key = 0;
            Essence topEssence;
            Essence lowEssence;
            if (essence1.ElementCount >= essence2.ElementCount)
            {
                topEssence = essence1;
                lowEssence = essence2;
            }
            else
            {
                topEssence = essence2;
                lowEssence = essence1;
            }


            key = topEssence.EssenceId;
            if (topEssence.ElementCount > 1)
                key <<= 4;
            key |= lowEssence.EssenceId;

            Debug.Log(Convert.ToString(key,2).PadLeft(16,'0'));
            if (recipes.ContainsKey(key))
            {
                Debug.Log(recipes[key].name);
                return recipes[key];
            }
            else
                return null;
        }
    }
}