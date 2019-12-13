using System;
using Data.Data_Types;
using Data.Interfaces.Player;
using UnityEngine;

namespace DataBehaviors.Game.PlayerInput
{
    public class MouseKeyboardPlayerInput : IPlayerInput
    {
        public event Action OnFirePressed = delegate {  };
        public event Action OnAirPressed = delegate {  };
        public event Action OnWaterPressed = delegate {  };
        public event Action OnEarthPressed = delegate {  };
        public event Action OnStartPlacingObeliskPressed = delegate {  };
        public event Action OnPrimaryKeyPressed = delegate {  };
        public event Action OnSecondaryKeyPressed = delegate {  };
        public event Action OnUtilityKeyPressed = delegate {  };
        public event Action OnIncreasePressed = delegate {  };
        public event Action OnDecreasePressed = delegate {  };
        public event Action OnPrimaryKeyReleased = delegate {  };
        public event Action OnSecondaryKeyReleased = delegate {  };
        public event Action OnUtilityKeyReleased = delegate {  };

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                OnFirePressed();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                OnAirPressed();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                OnWaterPressed();
            if (Input.GetKeyDown(KeyCode.Alpha4))
                OnEarthPressed();
            if (Input.GetKeyDown(KeyCode.Alpha5))
                OnStartPlacingObeliskPressed();
            
            
            if (Input.GetMouseButtonDown(0))
                OnPrimaryKeyPressed();
            if (Input.GetMouseButtonDown(1))
                OnSecondaryKeyPressed();
            if (Input.GetMouseButtonUp(0))
                OnPrimaryKeyReleased();
            if (Input.GetMouseButtonUp(0))
                OnSecondaryKeyReleased();

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                OnIncreasePressed();
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                OnDecreasePressed();
        }
    }
}