using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;
    PlayerLocomotionManager locomotionManager;

    [Header("Player flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<PlayerLocomotionManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        isPerformingAction = animator.GetBool("isPerformingAction");
        isPerformingQuickTurn = animator.GetBool("isPerformingQuickTurn");
    }

    private void FixedUpdate()
    {
        locomotionManager.HandleAllLocomotion();
    }
}
