using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity Resource Data", menuName = "Essence/Entity/Resource Data", order = 1)]
public class TowerResourceData : ScriptableObject
{
    public int BuildCost;
    public float TimeToBuild;
    public int RefundAmount;
}
