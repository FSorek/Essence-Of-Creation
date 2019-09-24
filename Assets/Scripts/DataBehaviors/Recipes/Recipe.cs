using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public byte Tier { get; private set; }
    public int Id { get; private set; }

    public Recipe(List<BaseElement> elementChain)
    {
        Id = TowerRecipeId.GetID(elementChain);
        Tier = 1;
        for (int i = 1; i <= 3; i++)
        {
            if (Id >= Mathf.Pow(16, i))
                Tier++;
        }
    }
}
