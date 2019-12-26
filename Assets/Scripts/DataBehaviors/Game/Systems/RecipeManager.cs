using System.Collections.Generic;
using Data.Data_Types;
using Data.Data_Types.Enums;
using DataBehaviors.Recipes;
using Monobehaviors.Essences;
using UnityEngine;

namespace DataBehaviors.Game.Systems
{
    public class RecipeManager : MonoBehaviour // static ?
    {
        public static RecipeManager Instance;
        public ForgedEssence[] PlayableObelisks;
        private readonly Dictionary<int, ForgedEssence> recipes = new Dictionary<int, ForgedEssence>();
        public static Dictionary<int, ForgedEssence> Recipes => Instance.recipes;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError("RecipeManager instance singleton set more than once!");

            foreach (var playableTower in PlayableObelisks)
            {
                int recipeId = TowerRecipeId.GetID(playableTower.InfusedElements);
                recipes.Add(recipeId, playableTower);
            }
        }

        public ForgedEssence GetTowerFromPath(List<BaseElement> path)
        {
            int id = TowerRecipeId.GetID(path);
            return recipes.ContainsKey(id) ? recipes[id] : null;
        }

        public ForgedEssence GetTowerFromPath(List<BaseElement> currentPath, BaseElement element)
        {
            int id = TowerRecipeId.GetID(currentPath, element);
            return recipes.ContainsKey(id) ? recipes[id] : null;
        }
    }
}