using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    PursueTargetState targetState;

    [Header("Zombie Attack")]
    public ZombieAttackAction[] zombieAttackActions;

    [Header("Potential Attacks Performable right now")]
    public List<ZombieAttackAction> potentialAttacks;

    [Header("Current Attack now playing")]
    public ZombieAttackAction currentAttack;

    [Header("State Flags")]
    public bool hasPerformedAttack;

    private void Awake()
    {
        targetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        Debug.Log("Attack");

        zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);

        if(zombieManager.isPerformingAction)
        {
            zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        if(!hasPerformedAttack && zombieManager.attackCooldownTimer <= 0)
        {
            if(currentAttack != null)
            {
                AttackTarget(zombieManager);
            }
            else
            {
                GetAttack(zombieManager);
            }

        }

        if (hasPerformedAttack)
        {
            //reset state
            resetStateFlag();
            return targetState;
        }
        else
        {
            return this;
        }

    }

    private void GetAttack(ZombieManager zombieManager)
    {
        for(int i = 0; i < zombieAttackActions.Length; i++)
        {
            ZombieAttackAction zombieAttack = zombieAttackActions[i];

            if(zombieManager.distanceFromCurrentTarget <= zombieAttack.maxiumAttackDistance
                && zombieManager.distanceFromCurrentTarget >= zombieAttack.miniumAttackDistance)
            {
                if (zombieManager.viewableAngleFromCurrentTarget <= zombieAttack.maxiumAttackAngle
                && zombieManager.viewableAngleFromCurrentTarget >= zombieAttack.miniumAttackAngle)
                {
                    potentialAttacks.Add(zombieAttack);
                }
            }
        }

        int randomValue = Random.Range(0, potentialAttacks.Count);

        if(potentialAttacks.Count > 0 )
        {
            currentAttack = potentialAttacks[randomValue];
            potentialAttacks.Clear();
        }
    }

    private void AttackTarget(ZombieManager zombieManager)
    {
        if(currentAttack != null)
        {
            hasPerformedAttack = true;
            zombieManager.attackCooldownTimer = currentAttack.attackCooldown;
            zombieManager.zombieAnimatorManager.PlayTargetAttackAnimation(currentAttack.attackAnimation);
        }
        else
        {
            Debug.LogWarning("WARNING: attempting to perform an attack, but has no attack");
        }
    }

    private void resetStateFlag()
    {
        hasPerformedAttack = false;
    }
}


