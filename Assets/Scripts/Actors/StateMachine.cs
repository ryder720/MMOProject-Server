using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState currentState;
    public event Action<BaseState> onStateChanged;

    private void Update()
    {
        if (currentState == null)
        {
            currentState = _availableStates.Values.First();
        }
        var nextState = currentState?.Tick();

        if(nextState != null &&
            nextState != currentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        currentState = _availableStates[nextState];
        onStateChanged?.Invoke(currentState);
    }
}
