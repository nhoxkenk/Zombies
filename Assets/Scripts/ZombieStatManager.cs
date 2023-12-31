using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour
{
    ZombieManager m_zombieManager;

    [Header("Damage Modifiers")]
    public float headshotDamageMultiplier = 1.5f;

    [Header("Overall Health")]
    public int overallHealth = 100;

    [Header("Head Health")]
    public int headHealth = 100;

    [Header("Upperbody Health")]
    public int torsoHealth = 100;
    public int leftArmHealth = 100;
    public int rightArmHealth = 100;

    [Header("Lower Body Health")]
    public int leftLegHealth = 100;
    public int rightLegHealth = 100;

    private void Awake()
    {
        m_zombieManager = GetComponent<ZombieManager>();
    }

    public void DamageDealToHead(int damage)
    {
        headHealth -= Mathf.RoundToInt(damage * headshotDamageMultiplier);
        overallHealth -= Mathf.RoundToInt(damage * headshotDamageMultiplier);
        CheckForDeath();
    }

    public void DamageDealToTorso(int damage)
    {
        torsoHealth -= damage;
        overallHealth -= damage;
        CheckForDeath();
    }

    public void DamageDealToArm(bool isLeftArm, int damage)
    {
        if(isLeftArm)
        {
            leftArmHealth -= damage;
        }
        else
        {
            rightArmHealth -= damage;
        }
        CheckForDeath();
    }

    public void DamageDealToLeg(bool isLeftLeg, int damage)
    {
        if (isLeftLeg)
        {
            leftLegHealth -= damage;
        }
        else
        {
            rightLegHealth -= damage;
        }
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        if(overallHealth < 0)
        {
            overallHealth = 0;
            m_zombieManager.isDead = true;
            m_zombieManager.zombieAnimatorManager.PlayTargetActionAnimation("Zombie Death");
        }
    }


}
