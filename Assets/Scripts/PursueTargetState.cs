using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }
    public override State Tick(ZombieManager zombieManager)
    {
        //cannot do two things at the same time, like playing hit animation along with run, etc...
        if (zombieManager.isPerformingAction)
        {
            RotateTowardsTargetWhileAttacking(zombieManager);
            zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        Debug.Log("RUNNING the pursue target state");
        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsTarget(zombieManager);

        if(zombieManager.distanceFromCurrentTarget <= zombieManager.maximumAttackDistance)
        {
            zombieManager.agent.enabled = false;
            return attackState;
        }
        else
        {
            return this;
        }
        
    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsTarget(ZombieManager zombieManager)
    {
        if (zombieManager.canRotate)
        {
            zombieManager.agent.enabled = true;
            zombieManager.agent.SetDestination(zombieManager.currentTarget.transform.position);
            zombieManager.transform.rotation = Quaternion.Lerp(zombieManager.transform.rotation,
                zombieManager.agent.transform.rotation, zombieManager.rotationSpeed / Time.deltaTime);
        }
        
    }

    private void RotateTowardsTargetWhileAttacking(ZombieManager zombieManager)
    {
        if(zombieManager.canRotate)
        {
            Vector3 direction = zombieManager.currentTarget.transform.position - zombieManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = zombieManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, targetRotation, zombieManager.rotationSpeed * Time.deltaTime);
        }
    }
}
