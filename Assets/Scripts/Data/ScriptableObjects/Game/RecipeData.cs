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
            }
        }

        public Essence TryMerge(Essence essence1, Essence essence2)
        {
            int key = EssenceIdGenerator.GetID(essence1, essence2);

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