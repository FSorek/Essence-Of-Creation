using System;
using Data.Data_Types;
using UnityEngine;

namespace Data.Interfaces.Player
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "Essence/Player/Input")]
    public class PlayerInput : ScriptableObject
    {
        public KeyCode FireKey;
        public KeyCode AirKey;
        public KeyCode WaterKey;
        public KeyCode EarthKey;
        public KeyCode PlaceObeliskKey;
        public KeyCode PrimaryKey;
        public KeyCode SecondaryKey;
        public KeyCode UtilityKey;

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
        

        public void ListenToInput()
        {
            if (Input.GetKeyDown(FireKey))
                OnFirePressed();
            if (Input.GetKeyDown(AirKey))
                OnAirPressed();
            if (Input.GetKeyDown(WaterKey))
                OnWaterPressed();
            if (Input.GetKeyDown(EarthKey))
                OnEarthPressed();
            if (Input.GetKeyDown(PlaceObeliskKey))
                OnStartPlacingObeliskPressed();
            
            
            if (Input.GetKeyDown(PrimaryKey))
                OnPrimaryKeyPressed();
            if (Input.GetKeyDown(SecondaryKey))
                OnSecondaryKeyPressed();
            if (Input.GetKeyUp(PrimaryKey))
                OnPrimaryKeyReleased();
            if (Input.GetKeyUp(SecondaryKey))
                OnSecondaryKeyReleased();

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                OnIncreasePressed();
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                OnDecreasePressed();
        }
    }
}