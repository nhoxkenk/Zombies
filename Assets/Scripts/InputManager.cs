using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    Animator animator;
    PlayerManager playerManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput;

    [Header("Button Inputs")]
    public bool runInput;
    public bool quickTurnInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovements.Movements.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovements.Run.performed += i => runInput = true;
            playerControls.PlayerMovements.Run.canceled += i => runInput = false;
            playerControls.PlayerMovements.QuickTurn.performed += i => quickTurnInput = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleQuickTurnInput();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animatorManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);
    }

    private void HandleQuickTurnInput()
    {
        if(playerManager.isPerformingAction)
        {
            return;
        }

        if(quickTurnInput)
        {
            animator.SetBool("isPerformingQuickTurn", true);
            animatorManager.PlayAnimationWithoutRootMotion("Quick Turn",true);
        }

    }
}
