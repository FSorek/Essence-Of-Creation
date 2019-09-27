using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class RecipeManager : MonoBehaviour // static ?
{
    public Obelisk[] PlayableObelisks;
    public static Dictionary<int, Obelisk> Recipes => Instance.recipes;
    private Dictionary<int, Obelisk> recipes = new Dictionary<int, Obelisk>();
    public static RecipeManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("RecipeManager instance singleton set more than once!");
        }

        foreach (var playableTower in PlayableObelisks)
        {
            var recipeId = TowerRecipeId.GetID(playableTower.InfusedElements);
            recipes.Add(recipeId, playableTower);
        }
    }

    public Obelisk GetTowerFromPath(List<BaseElement> path)
    {
        var id = TowerRecipeId.GetID(path);
        return recipes.ContainsKey(id) ? recipes[id] : null;
    }

    public Obelisk GetTowerFromPath(List<BaseElement> currentPath, BaseElement element)
    {
        var id = TowerRecipeId.GetID(currentPath, element);
        return recipes.ContainsKey(id) ? recipes[id] : null;
    }
}

