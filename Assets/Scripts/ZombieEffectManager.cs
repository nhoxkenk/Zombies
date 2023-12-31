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

    public void ZombieDamagedHead(int damage)
    {
        if(!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
            zombieManager.zombieStatManager.DamageDealToHead(damage);
        }
        
    }

    public void ZombieDamagedTorso(int damage)
    {
        if (!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Torso", 0.2f);
            zombieManager.zombieStatManager.DamageDealToTorso(damage);
        }
        
    }

    public void ZombieDamagedLeftArm(int damage)
    {
        if (!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
            zombieManager.zombieStatManager.DamageDealToArm(true, damage);
        }
        
    }

    public void ZombieDamagedRightArm(int damage)
    {
        if (!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
            zombieManager.zombieStatManager.DamageDealToArm(false, damage);
        }
        
    }

    public void ZombieDamagedLeftLeg(int damage)
    {
        if (!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
            zombieManager.zombieStatManager.DamageDealToLeg(true, damage);
        }
        
    }

    public void ZombieDamagedRightLeg(int damage)
    {
        if (!zombieManager.isDead)
        {
            zombieManager.isPerformingAction = true;
            zombieManager.animator.CrossFade("Zombie Hit Head", 0.2f);
            zombieManager.zombieStatManager.DamageDealToLeg(false, damage);
        }
        
    }
}
