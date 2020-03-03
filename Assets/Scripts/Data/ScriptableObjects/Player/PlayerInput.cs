using System;
using UnityEngine;

namespace Data.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "Essence/Player/Input")]
    public class PlayerInput : ScriptableObject
    {
        [SerializeField] private KeyCode FireKey;
        [SerializeField] private KeyCode AirKey;
        [SerializeField] private KeyCode WaterKey;
        [SerializeField] private KeyCode EarthKey;
        [SerializeField] private KeyCode PlaceObeliskKey;
        [SerializeField] private KeyCode WeaveEssenceKey;
        [SerializeField] private KeyCode PrimaryKey;
        [SerializeField] private KeyCode SecondaryKey;
        [SerializeField] private KeyCode UtilityKey;

        [SerializeField] private string VerticalAxis;
        [SerializeField] private string HorizontalAxis;

        public float Vertical => Input.GetAxis(VerticalAxis);
        public float Horizontal => Input.GetAxis(HorizontalAxis);

        public event Action OnFirePressed = delegate {  };
        public event Action OnAirPressed = delegate {  };
        public event Action OnWaterPressed = delegate {  };
        public event Action OnEarthPressed = delegate {  };
        public event Action OnStartPlacingObeliskPressed = delegate {  };
        public event Action OnWeaveEssencePressed = delegate {  };
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
            if (Input.GetKeyDown(WeaveEssenceKey))
                OnWeaveEssencePressed();
            
            
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