using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponLoaderSlot : MonoBehaviour
{
    public GameObject currentWeaponModel;
    public GameObject weaponPose;
    public GameObject weaponAiming; 

    private void UnloadAndDestroyWeapon()
    {
        if(currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    private void ChangeTransform(Transform transform1, Transform transform2)
    {
        transform1.position = transform2.position;
        transform1.rotation = transform2.rotation;
    }

    public void SetConstraintWeapon()
    {
        MultiParentConstraint parentConstraint = weaponPose.GetComponent<MultiParentConstraint>();
        MultiParentConstraint parentConstraintAiming = weaponAiming.GetComponent<MultiParentConstraint>();

        MultiPositionConstraint multiPositionConstraintPose = weaponPose.GetComponent<MultiPositionConstraint>();
        MultiPositionConstraint multiPositionConstraintAiming = weaponAiming.GetComponent<MultiPositionConstraint>();

        Transform weaponPoseTransform = currentWeaponModel.transform.Find("Weapon_Transform");
        string parentWeaponName = weaponPoseTransform.parent.name;
        parentWeaponName = parentWeaponName.Substring(0, parentWeaponName.Length - 7);

        switch (parentWeaponName)
        {
            case "Weapon_Rifle":
                multiPositionConstraintPose.data.offset = new Vector3(0.05f, -0.2f, 0.2f);
                multiPositionConstraintAiming.data.offset = new Vector3(0.05f, 0f, 0.3f);
                break;
            case "Weapon_Pistol":
                multiPositionConstraintPose.data.offset = new Vector3(0.1f, 0.05f, 0.2f);
                multiPositionConstraintAiming.data.offset = new Vector3(0.05f, 0.1f, 0.4f);
                break;
        }



        parentConstraint.data.constrainedObject = weaponPoseTransform;
        parentConstraintAiming.data.constrainedObject = weaponPoseTransform;
        ChangeTransform(weaponPose.transform, weaponPoseTransform);
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
        weaponModel.transform.localScale = Vector3.one;
        currentWeaponModel = weaponModel;

        SetConstraintWeapon();
    }
}
