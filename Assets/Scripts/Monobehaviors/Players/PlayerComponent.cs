using Data.ScriptableObjects.Player;
using UnityEngine;

namespace Monobehaviors.Players
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void Update()
        {
            playerInput.ListenToInput(); // to-do: think if this can be replaced 
        }
    }
}