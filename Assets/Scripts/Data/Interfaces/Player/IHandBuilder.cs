using System.Collections;
using UnityEngine;

public interface IHandBuilder
{
    void Build(Elements element, PlayerBuildingData data, BuildSpot buildSpot);
}