using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponLoaderSlot : MonoBehaviour
{
    public GameObject currentWeaponModel;

    private void UnloadAndDestroyWeapon()
    {
        if(currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItem weaponItem)
    {
        UnloadAndDestroyWeapon();

        if(weaponItem ==  null)
        {
            return;
        }

        GameObject weaponModel = Instantiate(weaponItem.itemModel, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;  
        weaponModel.transform.localScale = Vector3.one * 100;
        currentWeaponModel = weaponModel;
    }
}
