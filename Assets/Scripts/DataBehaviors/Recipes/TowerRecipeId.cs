using System.Collections.Generic;
using Data.Data_Types;

namespace DataBehaviors.Recipes
{
    public static class TowerRecipeId
    {
        public static int GetID(List<BaseElement> elementChain)
        {
            int id = 0;
            for (int i = 0; i < elementChain.Count; i++)
            {
                if (i > 1) id <<= 4;
                id |= (int) elementChain[i];
            }

            return id;
        }

        public static int GetID(List<BaseElement> elementChain, BaseElement lastElement)
        {
            int id = 0;
            var fullElements = new List<BaseElement>(elementChain);
            fullElements.Add(lastElement);
            for (int i = 0; i < fullElements.Count; i++)
            {
                if (i > 1) id <<= 4;
                id |= (int) fullElements[i];
            }

            return id;
        }
    }
}