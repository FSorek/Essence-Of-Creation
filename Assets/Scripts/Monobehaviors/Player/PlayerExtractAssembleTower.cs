using System.Collections.Generic;
using DataBehaviors.Game.Systems;
using DataBehaviors.Player.States;
using Monobehaviors.BuildSpot;
using Monobehaviors.Tower;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class PlayerExtractAssembleTower : MonoBehaviour
    {
        public Stack<GameObject> ExtractedEssences { get; } = new Stack<GameObject>(2);

        private void Awake()
        {
            //MergeAttackPlayerState.OnExtractTowerFinished += ExtractionFinished;
            //MergeAttackPlayerState.OnAssembleTowerFinished += AssembleFinished;
        }

        private void AssembleFinished(AttractionSpot spot)
        {
            if (ExtractedEssences.Count <= 0)
                return;
            var forgedEssence = ExtractedEssences.Pop();
            spot.AssignEssence(forgedEssence);
            forgedEssence.transform.position = spot.transform.position;
            forgedEssence.SetActive(true);
        }

        private void ExtractionFinished(BuildSpot.AttractionSpot spot)
        {
            var extractedTower = spot.CurrentEssence;
            extractedTower.SetActive(false);
            ExtractedEssences.Push(extractedTower);
            spot.ClearCurrentEssence();

            if (ExtractedEssences.Count >= 2)
            {
                var inA = ExtractedEssences.Pop().GetComponent<ForgedEssence>();
                var inB = ExtractedEssences.Peek().GetComponent<ForgedEssence>();

                ForgedEssence result = null;
                if (inB.InfusedElements.Count == 1)
                    result = RecipeManager.Instance.GetTowerFromPath(inA.InfusedElements, inB.InfusedElements[0]);
                else if (inB.InfusedElements.Count > 1 && inA.InfusedElements.Count == 1)
                    result = RecipeManager.Instance.GetTowerFromPath(inB.InfusedElements, inA.InfusedElements[0]);
                if (result == null)
                {
                    ExtractedEssences.Push(inA.gameObject);
                }
                else
                {
                    ExtractedEssences.Pop();
                    var newTower = Instantiate(result);
                    newTower.gameObject.SetActive(false);
                    ExtractedEssences.Push(newTower.gameObject);
                }
            }
        }
    }
}