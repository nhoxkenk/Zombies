using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursueTargetState pursueTargetState;

    //The layer used to detect potential targets to attack
    [Header("Detection Layer")]
    [SerializeField] LayerMask detectionLayer;

    [Header("Line of sight detection")]
    [SerializeField]float characterEyeHeight = 1.7f;
    [SerializeField] LayerMask ignoreForLineOfSight;

    //How far away we can detect a target
    [Header("Detection Radius")]
    [SerializeField] float detectionRadius = 5;

    //How wide we can see a target within our FIELD OF VIEW
    [Header("Detection Angle Radius")]
    [SerializeField] float miniumDetectionRadiusAngle = -100f;
    [SerializeField] float maxiumDetectionRadiusAngle = 180f;

    //Character idle until they find a potential target
    //If a target is found we proceed to the "Pursue target" state
    //If no target is found we remain in the idle state

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }
    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.currentTarget != null)
        {
            Debug.Log("Found a target!");
            return pursueTargetState;
        }
        else
        {
            Debug.Log("Not found any target!");
            FindATargetViaLineOfSight(zombieManager);
            return this;
        }
    }

    private void FindATargetViaLineOfSight(ZombieManager zombieManager)
    {
        //Searching all colliders on the layer of the PLAYER within a certain radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        Debug.Log("Check for player collider");
        //For every collider that we find, that is on the same layer of the player, we can try and search it for a PlayerManager script
        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            if (player != null)
            {
                Debug.Log("Found the player collider ");
                //The target must be in front of us
                Vector3 targetDirection = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > miniumDetectionRadiusAngle && viewableAngle < maxiumDetectionRadiusAngle)
                {

                    Debug.Log("We have passed the field of view check");

                    RaycastHit hit;

                    //Secure that the line does not hit the floor
                    Vector3 playerStartPoint = new Vector3(player.transform.position.x, characterEyeHeight, player.transform.position.z);
                    Vector3 zombieStartPoint = new Vector3(transform.position.x, characterEyeHeight, transform.position.z);

                    Debug.DrawLine(playerStartPoint, zombieStartPoint, Color.red);

                    //Check one last time for object block zombie view
                    if(Physics.Linecast(zombieStartPoint, playerStartPoint, out hit, ignoreForLineOfSight))
                    {
                        Debug.Log("There is something in the way ");
                    }
                    else
                    {
                        Debug.Log("We had found the target, switching state ");
                        zombieManager.currentTarget = player;
                    }

                    
                }
            }
        }
    }
}
