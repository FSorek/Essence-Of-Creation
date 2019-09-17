using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Obelisk : GameEntity
{
    public Vector3 AttackSpawnPosition => transform.position;

    public List<BaseElement> InfusedElements = new List<BaseElement>();
}