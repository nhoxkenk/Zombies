using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public ZombieAnimatorManager zombieAnimatorManager;
    public ZombieStatManager zombieStatManager;
    public IdleState startingState;

    [Header("Flag")]
    public bool isPerformingAction;
    public bool isDead;

    [Header("Current state")]
    private State currentState;

    [Header("Current target")]
    public PlayerManager currentTarget;
    public Vector3 targetDirection;
    public float distanceFromCurrentTarget;
    public float viewableAngleFromCurrentTarget;

    public Animator animator;

    public NavMeshAgent agent;

    public Rigidbody zombieRigidbody;

    [Header("Locomotion")]
    public float rotationSpeed = 5;

    [Header("Attack")]
    public float attackCooldownTimer;
    public float miniumAttackDistance = 0.5f;
    public float maximumAttackDistance = 1f;

    private void Awake()
    {
        currentState = startingState;
        animator = GetComponent<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
        zombieAnimatorManager = GetComponent<ZombieAnimatorManager>();
        zombieStatManager = GetComponent<ZombieStatManager>();
    }
    private void FixedUpdate()
    {
        if(!isDead)
            HandleStateMachine();
    }
    private void Update()
    {
        agent.transform.localPosition = Vector3.zero;

        if(attackCooldownTimer > 0 )
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if(currentTarget != null)
        {
            targetDirection = currentTarget.transform.position - transform.position;
            viewableAngleFromCurrentTarget = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
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
