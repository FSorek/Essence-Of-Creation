using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    private Dictionary<Type, UnitState> availableStates;
    public UnitState CurrentState { get; private set; }
    public event Action<UnitState> OnStateChanged;

    public void SetStates(Dictionary<Type, UnitState> dict)
    {
        availableStates = dict;
    }

    public void Tick()
    {
        if (CurrentState == null)
            CurrentState = availableStates.Values.First();
        var nextState = CurrentState?.Tick();
        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
