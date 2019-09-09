using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obelisk Resource Data", menuName = "Essence/Obelisk/Resource Data", order = 1)]
public class TowerResourceData : ScriptableObject
{
    public int BuildCost;
    public float TimeToBuild;
    public int RefundAmount;
}
