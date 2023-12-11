using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEffectManager : MonoBehaviour
{
    ZombieManager zombieManager;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();    
    }

    public void ZombieDamagedHead()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
    }

    public void ZombieDamagedTorso()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Torso", 0.2f);
    }

    public void ZombieDamagedLeftArm()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
    }

    public void ZombieDamagedRightArm()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
    }

    public void ZombieDamagedLeftLeg()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
    }

    public void ZombieDamagedRightLeg()
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
    }
}
