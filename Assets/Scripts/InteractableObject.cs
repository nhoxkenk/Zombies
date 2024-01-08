using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The base class for interactable object (Items, Doors, Levers, Etc,...)

    protected PlayerManager playerManager; //The player interacting with the object
    [SerializeField] protected GameObject interactableImage; //The image indicating whether player can interact or not
    protected Collider interactableCollider; //Space to enabling interaction

    protected virtual void OnTriggerEnter(Collider other)
    {
        //OPTIONAL: Check for specific layer of collider
        if(playerManager == null)
        {
            playerManager = other.GetComponent<PlayerManager>();
        }

        if(playerManager != null)
        {
            interactableImage.SetActive(true);
            playerManager.canInteract = true;
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(playerManager != null)
        {
            if (playerManager.inputManager.interactInput)
            {
                Interact(playerManager);
                playerManager.inputManager.interactInput = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (playerManager == null)
        {
            playerManager = other.GetComponent<PlayerManager>();
        }

        if (playerManager != null)
        {
            interactableImage.SetActive(false);
            playerManager.canInteract = false;
        }
    }

    protected virtual void Interact(PlayerManager playerManager)
    {
        Debug.Log("You have interacted");
    }
}
