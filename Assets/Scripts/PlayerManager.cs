using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;
    PlayerLocomotionManager locomotionManager;
    PlayerEquipmentManager equipmentManager;

    [Header("Player flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool isAiming;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<PlayerLocomotionManager>();
        animator = GetComponent<Animator>();
        equipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        isPerformingAction = animator.GetBool("isPerformingAction");
        isPerformingQuickTurn = animator.GetBool("isPerformingQuickTurn");
        isAiming = animator.GetBool("isAiming");
    }

    private void FixedUpdate()
    {
        locomotionManager.HandleAllLocomotion();
    }

    public void UseCurrentWeapon()
    {
        equipmentManager.weaponAnimatorManager.ShootWeapon();
    }
}
