using System;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Player.Particles;
using UnityEngine;

namespace Monobehaviors.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerBuildData playerBuildData;

        private void Update()
        {
            playerInput.ListenToInput();
        }
    }
}