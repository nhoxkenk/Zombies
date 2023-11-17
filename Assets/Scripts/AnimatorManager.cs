using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    RigBuilder rb;
    PlayerLocomotionManager manager;

    float snappedHorizontal;
    float snappedVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        manager = GetComponent<PlayerLocomotionManager>();
        rb = GetComponent<RigBuilder>();
    }

    public void PlayAnimationWithoutRootMotion(string targetAnimation, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, 0.1f);
    }

    public void HandleAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        if (horizontalMovement > 0)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }

        if (verticalMovement > 0)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }

        if(isRunning && snappedVertical > 0)
        {
            snappedVertical = 2;
        }

        animator.SetFloat("InputX", snappedHorizontal, 0.08f, Time.deltaTime);
        animator.SetFloat("InputY", snappedVertical, 0.08f, Time.deltaTime);

    }

    public void AssignHandIK(RightHandIKTarget rightTarget, LeftHandIKTarget leftTarget)
    {
        rightHandIK.data.target = rightTarget.transform;
        leftHandIK.data.target = leftTarget.transform;
        rb.Build();
    }

    private void OnAnimatorMove()
    {
        Vector3 animatorDeltaPosition = animator.deltaPosition;
        animatorDeltaPosition.y = 0;

        Vector3 velocity = animatorDeltaPosition / Time.deltaTime;
        manager.playerRigidbody.drag = 0;
        manager.playerRigidbody.velocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }
}
