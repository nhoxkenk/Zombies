using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    WeaponLoaderSlot weaponLoaderSlot;
    [Header("Current Equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimatorManager;
    RightHandIKTarget rightTarget;
    LeftHandIKTarget leftTarget;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
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
        animatorManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimatorManager = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightTarget = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftTarget = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        animatorManager.AssignHandIK(rightTarget, leftTarget);
    }
}
