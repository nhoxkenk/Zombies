using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    Animator animator;
    public AnimatorManager animatorManager;

    public PlayerUIManager playerUIManager;
    public PlayerLocomotionManager locomotionManager;
    public PlayerEquipmentManager equipmentManager;
    public PlayerInventoryManager inventoryManager;

    [Header("Player flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool isAiming;
    public bool canInteract;

    private void Awake()
    {
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<PlayerLocomotionManager>();
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
        equipmentManager = GetComponent<PlayerEquipmentManager>();
        inventoryManager = GetComponent<PlayerInventoryManager>();
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
        if (isPerformingAction)
        {
            return;
        }

        if(equipmentManager.weapon.remainingAmmo > 0 && playerUIManager != null)
        {
            equipmentManager.weaponAnimatorManager.ShootWeapon();
            equipmentManager.weapon.remainingAmmo--;
            playerUIManager.currentAmmoCountText.text = equipmentManager.weapon.remainingAmmo.ToString();
        }       
        else
        {
            Debug.Log("You are out of ammo, please reload weapon");
        }
    }

    public string GetCurrentWeaponName()
    {
        return equipmentManager.weapon.itemName;
    }
}
