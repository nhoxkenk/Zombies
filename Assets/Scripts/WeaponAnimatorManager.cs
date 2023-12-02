using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator weaponAnimator;

    public Transform raycastOrigin;
    public Transform raycastTarget;

    [Header("Weapon FX")]
    public ParticleSystem weaponMuzzleFlashFX;
    public ParticleSystem weaponBulletCaseFX;
    public ParticleSystem weaponDrill;

    Ray ray;
    RaycastHit hitInfo;

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        raycastTarget = GameObject.Find("CrossHairTarget").transform;
    }

    public void ShootWeapon()
    {
        weaponAnimator.Play("Shoot");

        //MUZZLE FLASH
        weaponMuzzleFlashFX.Play();
        //BULLE TSHELL
        weaponBulletCaseFX.Play();

        ray.origin = raycastOrigin.position;
        ray.direction = raycastTarget.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            weaponDrill.transform.position = hitInfo.point;
            weaponDrill.transform.forward = hitInfo.normal;
            weaponDrill.Play();
        }
    }
}
