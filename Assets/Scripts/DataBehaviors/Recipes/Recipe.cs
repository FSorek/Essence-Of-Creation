using System.Collections.Generic;
using Data.Data_Types;
using UnityEngine;

namespace DataBehaviors.Recipes
{
    public class Recipe
    {
        public Recipe(List<BaseElement> elementChain)
        {
            Id = TowerRecipeId.GetID(elementChain);
            Tier = 1;
            for (int i = 1; i <= 3; i++)
                if (Id >= Mathf.Pow(16, i))
                    Tier++;
        }

        public byte Tier { get; }
        public int Id { get; }
    }
}