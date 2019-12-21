using System;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public abstract class StateData<T> : ScriptableObject
    {
        [SerializeField] private T currentState;
        public event Action<T> OnStateEntered = delegate {  };
        public event Action<T> OnStateExit = delegate {  };
        public T CurrentState => currentState;

        public void ChangeState(T state)
        {
            OnStateExit(CurrentState);
            currentState = state;
            OnStateEntered(CurrentState);
        }
    }
}