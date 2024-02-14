using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatManager : MonoBehaviour
{
    [SerializeField] ZombieGrappleCollider rightHandGrappleCollider;
    [SerializeField] ZombieGrappleCollider leftHandGrappleCollider;
    private void Awake()
    {
        LoadGrappleColliders();
    }

    private void LoadGrappleColliders()
    {
        ZombieGrappleCollider[] grappleColliders = GetComponentsInChildren<ZombieGrappleCollider>();

        foreach(var grapple in grappleColliders)
        {
            if(grapple.isRightHandedGrappleCollider)
            {
                rightHandGrappleCollider = grapple;
            }
            else
            {
                leftHandGrappleCollider = grapple;
            }
        }

    }

    public void OpenGrappleColliders()
    {
        //Open collider
        //When if collider contacts player, player is locked into grapple animation
        rightHandGrappleCollider.enabled = true;
        leftHandGrappleCollider.enabled = true;
    }

    public void CloseGrappleColliders()
    {
        rightHandGrappleCollider.enabled = false;
        leftHandGrappleCollider.enabled = false;
    }
}
