using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState;

    public void Initialize(IState startState, Direction direction)
    {
        CurrentState = startState;
        CurrentState.Enter(direction);
    }
}
