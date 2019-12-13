using System;
using Data.Data_Types;
using UnityEngine;

namespace Data.Interfaces.Player
{
    public interface IPlayerInput
    {
        event Action OnFirePressed;
        event Action OnAirPressed;
        event Action OnWaterPressed;
        event Action OnEarthPressed;
        event Action OnStartPlacingObeliskPressed;
        event Action OnPrimaryKeyPressed;
        event Action OnSecondaryKeyPressed;
        event Action OnUtilityKeyPressed;
        event Action OnIncreasePressed;
        event Action OnDecreasePressed;
        
        
        
        event Action OnPrimaryKeyReleased;
        event Action OnSecondaryKeyReleased;
        event Action OnUtilityKeyReleased;
        
        
        
        void Tick();
    }
}