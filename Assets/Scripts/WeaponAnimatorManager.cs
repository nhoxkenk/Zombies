using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    PlayerManager playerManager;
    Animator weaponAnimator;

    public Transform raycastOrigin;
    public Transform raycastTarget;

    [Header("Weapon FX")]
    public ParticleSystem weaponMuzzleFlashFX;
    public ParticleSystem weaponBulletCaseFX;
    public ParticleSystem weaponDrill;

    [Header("Bullet Range")]
    public float bulletRange = 100f;

    [Header("Shootable Layer")]
    public LayerMask shootableLayer;

    Ray ray;
    RaycastHit hitInfo;

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        playerManager = GetComponentInParent<PlayerManager>();
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
        if(Physics.Raycast(ray, out hitInfo, bulletRange, shootableLayer))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            Debug.Log(hitInfo.collider.gameObject.layer);
            ZombieEffectManager zombieEffectManager = hitInfo.collider.gameObject.GetComponentInParent<ZombieEffectManager>();

            if(zombieEffectManager != null)
            {
                switch(hitInfo.collider.gameObject.layer)
                {
                    case 7:
                        {
                            zombieEffectManager.ZombieDamagedHead(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                    case 8:
                        {
                            zombieEffectManager.ZombieDamagedTorso(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                    case 9:
                        {
                            zombieEffectManager.ZombieDamagedRightArm(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                    case 10:
                        {
                            zombieEffectManager.ZombieDamagedLeftArm(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                    case 11:
                        {
                            zombieEffectManager.ZombieDamagedRightLeg(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                    case 12:
                        {
                            zombieEffectManager.ZombieDamagedLeftLeg(playerManager.equipmentManager.weapon.damage);
                            break;
                        }
                }
            }

            weaponDrill.transform.position = hitInfo.point;
            weaponDrill.transform.forward = hitInfo.normal;
            weaponDrill.Play();
        }
    }
}
