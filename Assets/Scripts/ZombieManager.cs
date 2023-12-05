using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public IdleState startingState;

    [Header("Current state")]
    private State currentState;

    [Header("Current target")]
    public PlayerManager currentTarget;
    public float distanceFromCurrentTarget;

    public Animator animator;

    public NavMeshAgent agent;

    public Rigidbody zombieRigidbody;

    [Header("Locomotion")]
    public float rotationSpeed = 5;

    [Header("Attack")]
    public float miniumAttackDistance = 1f;

    private void Awake()
    {
        currentState = startingState;
        animator = GetComponent<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        HandleStateMachine();
    }
    private void Update()
    {
        agent.transform.localPosition = Vector3.zero;

        if(currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
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
