using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        // if any is listening out on this event (hence the ?), if yes, invoke it
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInvSystem);
        interactSuccessful = true;
    }

    public void EndInteraction()
    {

    }
}
