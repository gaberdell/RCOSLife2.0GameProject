using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */

public class Interactor : MonoBehaviour
{
    //public playerAction playerControl;
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }

    /*
    private void Awake()
    {
        playerControl = new playerAction();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }
    */

    private void Update()
    {
        /* if we are walking around and our hitbox overlap with any object that have the "InteractionLayer", at
         * "InteractionPoint.position" and within the "InteractionPointRadius", our hitbox will be store in a collider 
         * type array "Physics.OverlapSphere()".
         */
        var collider = Physics2D.OverlapCircleAll(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        //fix so that the player can press any desire key to open chest
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                var interactable = collider[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    StartInteraction(interactable);
                }
            }
        }

        //check this again to see if it's WasPerformedThisFrame or WasPressedThisFrame
        /* bool interactingKeyPressed = playerControl.Player.Interacting.WasPressedThisFrame();

        if (interactingKeyPressed)
        {
            //Codes to be test (If there are no interactable items around, player can't call startInteracting method)
            /* 
            if(collider.Length != 0)
            {
                for (int i = 0; i < collider.Length; i++)
                {
                    var interactable = collider[i].GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        StartInteraction(interactable);
                    }
                }
            }
            else
            {
                EndInteraction();
            }
         

            for (int i = 0; i < collider.Length; i++)
            {
                var interactable = collider[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    StartInteraction(interactable);
                }
            }


            
        }
        */

    }


    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        // this bool will disable movement when player interact with interactable object/s 
        //in the player movement script (Not implement yet)
        IsInteracting = true;
    }

    // this bool will enable movement when player stop interact with interactable object/s 
    // in the player movement script (Not implement yet)
    /*
    void EndInteraction()
    {
        IsInteracting = false;
    }
    */

}
