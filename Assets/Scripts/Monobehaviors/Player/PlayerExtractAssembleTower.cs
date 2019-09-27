using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtractAssembleTower : MonoBehaviour
{
    public Stack<GameObject> ExtractedTowers => extractedTowers;
    private Stack<GameObject> extractedTowers = new Stack<GameObject>(2);

    private void Awake()
    {
        MergeAttackPlayerState.OnExtractTowerFinished += ExtractionFinished;
        MergeAttackPlayerState.OnAssembleTowerFinished += AssembleFinished;
    }

    private void AssembleFinished(BuildSpot spot)
    {
        if(extractedTowers.Count <= 0)
            return;
        var assembledTower = extractedTowers.Pop();
        spot.SetCurrentTower(assembledTower);
        assembledTower.transform.position = spot.Position;
        assembledTower.SetActive(true);
    }

    private void ExtractionFinished(BuildSpot spot)
    {
        var extractedTower = spot.CurrentTower;
        extractedTower.SetActive(false);
        extractedTowers.Push(extractedTower);
        spot.ClearCurrentTower();

        if (extractedTowers.Count >= 2)
        {
            var inA = extractedTowers.Pop().GetComponent<Obelisk>();
            var inB = extractedTowers.Peek().GetComponent<Obelisk>();

            Obelisk result = null;
            if (inB.InfusedElements.Count == 1)
                result = RecipeManager.Instance.GetTowerFromPath(inA.InfusedElements, inB.InfusedElements[0]);
            else if (inB.InfusedElements.Count > 1 && inA.InfusedElements.Count == 1)
                result = RecipeManager.Instance.GetTowerFromPath(inB.InfusedElements, inA.InfusedElements[0]);
            if (result == null)
            {
                extractedTowers.Push(inA.gameObject);
            }
            else
            {
                extractedTowers.Pop();
                var newTower = Instantiate(result);
                newTower.gameObject.SetActive(false);
                extractedTowers.Push(newTower.gameObject);
            }
        }
    }
}
