using System.Collections.Generic;
using DataBehaviors.Game.Systems;
using DataBehaviors.Player.States;
using Monobehaviors.Tower;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class PlayerExtractAssembleTower : MonoBehaviour
    {
        public Stack<GameObject> ExtractedTowers { get; } = new Stack<GameObject>(2);

        private void Awake()
        {
            MergeAttackPlayerState.OnExtractTowerFinished += ExtractionFinished;
            MergeAttackPlayerState.OnAssembleTowerFinished += AssembleFinished;
        }

        private void AssembleFinished(BuildSpot.BuildSpotComponent spotComponent)
        {
            if (ExtractedTowers.Count <= 0)
                return;
            var assembledTower = ExtractedTowers.Pop();
            spotComponent.SetCurrentTower(assembledTower);
            assembledTower.transform.position = spotComponent.transform.position;
            assembledTower.SetActive(true);
        }

        private void ExtractionFinished(BuildSpot.BuildSpotComponent spotComponent)
        {
            var extractedTower = spotComponent.CurrentTower;
            extractedTower.SetActive(false);
            ExtractedTowers.Push(extractedTower);
            spotComponent.ClearCurrentTower();

            if (ExtractedTowers.Count >= 2)
            {
                var inA = ExtractedTowers.Pop().GetComponent<Obelisk>();
                var inB = ExtractedTowers.Peek().GetComponent<Obelisk>();

                Obelisk result = null;
                if (inB.InfusedElements.Count == 1)
                    result = RecipeManager.Instance.GetTowerFromPath(inA.InfusedElements, inB.InfusedElements[0]);
                else if (inB.InfusedElements.Count > 1 && inA.InfusedElements.Count == 1)
                    result = RecipeManager.Instance.GetTowerFromPath(inB.InfusedElements, inA.InfusedElements[0]);
                if (result == null)
                {
                    ExtractedTowers.Push(inA.gameObject);
                }
                else
                {
                    ExtractedTowers.Pop();
                    var newTower = Instantiate(result);
                    newTower.gameObject.SetActive(false);
                    ExtractedTowers.Push(newTower.gameObject);
                }
            }
        }
    }
}