using System;
using Data.Data_Types;
using Data.Player;
using UnityEngine;

namespace Data.Interfaces.Player
{
    public interface IPlayer
    {
        Transform HandTransform { get; }
        Elements CurrentElement { get; }
        event Action<Elements> OnElementExecuted;
    }
}