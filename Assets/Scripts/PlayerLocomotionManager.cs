using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerLocomotionManager : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Rig aimingRig;
    public float aimDuration = 0.3f;
    PlayerManager playerManager;
    InputManager inputManager;
    Camera mainCamera;
    float yawCamera;

    public float RotationSpeed = 15f;
    public float quickTurnSpeed = 8f;

    [Header("Rotation Variables")]
    Quaternion targetRotation;
    Quaternion playerRotation;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllLocomotion()
    {

        HandleRotation();
    }

    private void HandleRotation()
    {
        if (inputManager.quickTurnInput)
        {
            inputManager.quickTurnInput = false;
        }

        if (playerManager.isAiming)
        {
            yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            targetRotation = Quaternion.Euler(0, yawCamera, 0);
            playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
            aimingRig.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            targetRotation = Quaternion.Euler(0, yawCamera, 0);
            playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            aimingRig.weight -= Time.deltaTime / aimDuration;
            if (inputManager.verticalMovementInput != 0 || inputManager.horizontalMovementInput != 0)
            {
                transform.rotation = playerRotation;
            }

            if (playerManager.isPerformingQuickTurn)
            {
                playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, quickTurnSpeed);
                transform.rotation = playerRotation;
            }
        }
       
    }

}
