using System.Collections.Generic;
using Data.Data_Types;
using Data.Data_Types.Enums;
using Monobehaviors.Essences;

namespace DataBehaviors.Recipes
{
    public static class EssenceIdGenerator
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

        public static int GetID(Essence essence1, Essence essence2)
        {
            int id = 0;
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


            id = topEssence.EssenceId;
            if (topEssence.ElementCount > 1)
                id <<= 4;
            id |= lowEssence.EssenceId;

            return id;
        }
    }
}