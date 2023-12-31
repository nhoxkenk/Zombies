using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Coroutine reloadCoroutine;

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
    public bool aimingInput;
    public bool shootingInput;
    public bool reloadInput;

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
            playerControls.PlayerActions.Aim.performed += i => aimingInput = true;
            playerControls.PlayerActions.Aim.canceled += i => aimingInput = false;
            playerControls.PlayerActions.Shoot.performed += i => shootingInput = true;
            playerControls.PlayerActions.Shoot.canceled += i => shootingInput = false;
            playerControls.PlayerActions.Reload.performed += i => reloadInput = true;
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
        HandleAimingInput();
        HandleShootingInput();
        HandleReloadInput();
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

    private void HandleAimingInput()
    {
        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            aimingInput = false;
            animator.SetBool("isAiming", false);
            animator.SetLayerWeight(2, 0.0f);
            return;
        }

        if (aimingInput)
        {
            animator.SetBool("isAiming", true);
            animator.SetLayerWeight(2, 1.0f);
        }
        else
        {
            animator.SetBool("isAiming", false);
            animator.SetLayerWeight(2, 0.0f);
        }

        animatorManager.UpdateAimConstraint();
    }

    private void HandleShootingInput()
    {
        //check if the weapon type is semi-auto or auto.
        string weaponName = playerManager.GetCurrentWeaponName();
        if (shootingInput && aimingInput)
        {
            if (weaponName != "Rifle")
                shootingInput = false;
            playerManager.UseCurrentWeapon();
        }
    }

    private void HandleReloadInput()
    {
        if(playerManager.isPerformingAction)
        {
            return;
        }

        if(reloadInput)
        {
            reloadInput = false;

            //check if weapon ammo if full
            if(playerManager.equipmentManager.weapon.remainingAmmo == playerManager.equipmentManager.weapon.maxAmmo)
            {
                Debug.Log("ALREADY FULL MAGAZINE");
                return;
            }

            //check ammo type for current weapon
            if(playerManager.inventoryManager.currentAmmoInInventory != null)
            {
                if(playerManager.inventoryManager.currentAmmoInInventory.ammoType == playerManager.equipmentManager.weapon.ammoType)
                {
                    int amountOfAmmoToReload = 0;
                    amountOfAmmoToReload = playerManager.equipmentManager.weapon.maxAmmo - playerManager.equipmentManager.weapon.remainingAmmo;

                    if(playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining >= amountOfAmmoToReload)
                    {
                        playerManager.equipmentManager.weapon.remainingAmmo += amountOfAmmoToReload;

                        playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining -= amountOfAmmoToReload;

                    }
                    else
                    {
                        playerManager.equipmentManager.weapon.remainingAmmo = playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining;

                        playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining = 0;

                    }

                    //animation smooth
                    animator.SetLayerWeight(2, 0.0f);
                    playerManager.animatorManager.ClearHandIK();
                    playerManager.animatorManager.PlayAnimation("Reload_Pistol", true);
                    //place more ammo in the weapon
                    playerManager.playerUIManager.currentAmmoCountText.text = playerManager.equipmentManager.weapon.remainingAmmo.ToString();
                    playerManager.playerUIManager.reservedAmmoCountText.text = playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining.ToString();
                    reloadCoroutine = StartCoroutine(WaitForReloadAnimation());
                }
            }

            
        }
    }

    private IEnumerator WaitForReloadAnimation()
    {

        yield return new WaitForSeconds(1.75f);

        playerManager.animatorManager.RefreshHandIK();

        StopCoroutine(reloadCoroutine);
    }
}
