using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGrappleCollider : MonoBehaviour
{
    ZombieManager zombie;
    public Collider gameCollider;
    public bool isRightHandedGrappleCollider;

    private Coroutine grappleCoroutine;

    private void Awake()
    {
        zombie = GetComponentInParent<ZombieManager>();
        gameCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();

            if(playerManager != null)
            {
                if(!playerManager.isPerformingAction && !zombie.isDead)
                {
                    zombie.zombieAnimatorManager.PlayGrappleAnimation("Zombie Grap", true);
                    zombie.animator.SetFloat("Vertical", 0);

                    playerManager.playerAnimatorManager.ClearRigLayerHand();
                    playerManager.playerAnimatorManager.PlayAnimation("Victim", true);
                    grappleCoroutine = StartCoroutine(WaitForGrappleAnimation(playerManager));

                    Quaternion targetZombieRotation = Quaternion.LookRotation(playerManager.transform.position - zombie.transform.position);
                    zombie.transform.rotation = targetZombieRotation;

                    Quaternion targetPlayerRotation = Quaternion.LookRotation(zombie.transform.position - playerManager.transform.position);
                    playerManager.transform.rotation = targetPlayerRotation;
                }
            }
        }
    }

    private IEnumerator WaitForGrappleAnimation(PlayerManager playerManager)
    {

        yield return new WaitForSeconds(2.5f);

        playerManager.playerAnimatorManager.RefreshRigLayerHand();

        StopCoroutine(grappleCoroutine);
    }
}
