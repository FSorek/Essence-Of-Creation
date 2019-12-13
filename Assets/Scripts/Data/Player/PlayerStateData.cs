using System;
using Data.Data_Types;
using UnityEngine;

namespace Monobehaviors.Player
{
    [CreateAssetMenu(fileName = "Player State Data", menuName = "Essence/Player/State Data")]
    public class PlayerStateData : ScriptableObject
    {
        public event Action<PlayerStates> OnStateEntered = delegate {  };
        public event Action<PlayerStates> OnStateExit = delegate {  };
        public PlayerStates CurrentState;

        public void ChangeState(PlayerStates state)
        {
            OnStateExit(CurrentState);
            CurrentState = state;
            OnStateEntered(CurrentState);
        }
    }
}