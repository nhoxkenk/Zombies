using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    [Header("Is Performing Action Bool")]
    public string isPerformingAction = "isPerformingAction";
    public bool isPerformingActionStatus = false;

    [Header("Is Performing Quick Turn")]
    public string isPerformingQuickTurn = "isPerformingQuickTurn";
    public bool isPerformingQuickTurnStatus = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isPerformingAction, isPerformingActionStatus);
        animator.SetBool(isPerformingQuickTurn, isPerformingQuickTurnStatus);
    }


}
