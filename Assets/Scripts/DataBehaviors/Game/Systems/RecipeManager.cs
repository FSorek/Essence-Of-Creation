using System.Collections.Generic;
using Data.Data_Types;
using DataBehaviors.Recipes;
using Monobehaviors.Tower;
using UnityEngine;

namespace DataBehaviors.Game.Systems
{
    public class RecipeManager : MonoBehaviour // static ?
    {
        public static RecipeManager Instance;
        public Obelisk[] PlayableObelisks;
        private readonly Dictionary<int, Obelisk> recipes = new Dictionary<int, Obelisk>();
        public static Dictionary<int, Obelisk> Recipes => Instance.recipes;


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

        public Obelisk GetTowerFromPath(List<BaseElement> path)
        {
            int id = TowerRecipeId.GetID(path);
            return recipes.ContainsKey(id) ? recipes[id] : null;
        }

        public Obelisk GetTowerFromPath(List<BaseElement> currentPath, BaseElement element)
        {
            int id = TowerRecipeId.GetID(currentPath, element);
            return recipes.ContainsKey(id) ? recipes[id] : null;
        }
    }
}