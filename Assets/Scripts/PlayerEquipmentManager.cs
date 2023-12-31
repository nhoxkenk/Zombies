using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager playerManager;
    WeaponLoaderSlot weaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimatorManager;
    RightHandIKTarget rightTarget;
    LeftHandIKTarget leftTarget;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        LoadWeaponLoaderSlot();
    }

    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlot()
    {
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        playerManager.animatorManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimatorManager = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightTarget = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftTarget = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        playerManager.animatorManager.AssignHandIK(rightTarget, leftTarget);
        playerManager.playerUIManager.currentAmmoCountText.text = weapon.remainingAmmo.ToString();

        if (playerManager.inventoryManager.currentAmmoInInventory != null)
        {
            if (playerManager.inventoryManager.currentAmmoInInventory.ammoType == playerManager.equipmentManager.weapon.ammoType)
            {
                playerManager.playerUIManager.reservedAmmoCountText.text = playerManager.inventoryManager.currentAmmoInInventory.ammoRemaining.ToString();
            }
        }
    }
}
