using System;
using UnityEngine;

public interface IPlayer
{
    Transform HandTransform { get; }
    PlayerBuildingData BuildingData { get; }
    PlayerVFXData VFXData { get; }
    Elements CurrentElement { get; }
    event Action<Elements> OnElementExecuted;
}
