using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorManager : MonoBehaviour
{
    ZombieManager zombieManager;
    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }

    public void PlayTargetAttackAnimation(string attackAnimation)
    {
        zombieManager.animator.applyRootMotion = true;
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade(attackAnimation, 0.2f);
    }
}
