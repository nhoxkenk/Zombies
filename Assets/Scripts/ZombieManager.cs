using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public IdleState startingState;

    [Header("Current state")]
    private State currentState;

    [Header("Current target")]
    public PlayerManager currentTarget;

    private void Awake()
    {
        currentState = startingState;
    }
    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        //Run logic, based on which state we are currently on
        //If logic is met to switch to the next state, we change states

        State nextState;

        if(currentState != null)
        {
            nextState = currentState.Tick(this);

            if(nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
