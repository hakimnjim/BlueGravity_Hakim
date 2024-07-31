using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GlobalStateMachine : MonoBehaviour
{
    public Stack<State> stateHistoryStack = new Stack<State>();
    public State CurrentState;
    public State IdleState;

    public State InitState;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        Initialise();
    }

    public virtual void ChangeState(State newState = null)
    {
        if (CurrentState == newState)
        {
            return;
        }
        if (CurrentState != null)
        {
            CurrentState.Exit(); // Exit the current state
        }

        if (newState == null && stateHistoryStack.Count > 1)
        {
            stateHistoryStack.Pop(); // Pop the current state
            newState = stateHistoryStack.Peek(); // Get the previous state from the stack
        }

        if (newState != null)
        {
            if (!stateHistoryStack.Contains(newState))
            {
                stateHistoryStack.Push(newState); // Push the new state onto the stack
            }
            newState.Enter(this); // Enter the new state
            CurrentState = newState;
        }
    }

    public virtual void Initialise()
    {
        stateHistoryStack.Clear();
        CallChangeStateCoroutine(1, InitState);
    }

    private IEnumerator ChangeStateCoroutine(float seconds, State state = null)
    {
        yield return new WaitForSeconds(seconds);
        ChangeState(state);
    }

    public virtual void CallChangeStateCoroutine(float seconds, State state = null)
    {
        StartCoroutine(ChangeStateCoroutine(seconds, state));
    }
}
