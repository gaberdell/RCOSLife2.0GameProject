using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    protected override void Awake()
    {
        base.Awake();
        SaveGameManager.onLoadGame += LoadInventory;
    }

    private void LoadInventory(SaveData data)
    {
        // Check the save data for this specific chest inventory, and if its exist, load it
    }

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


[System.Serializable]
public struct ChestSaveData
{
    public InventorySystem invSystem;

    //not sure if this would work with 2D sapce. Gotta check it again
    public Vector3 position;
    public Quaternion rotation;

    public ChestSaveData(InventorySystem _invSystem, Vector3 pos, Quaternion rot)
    {
        invSystem = _invSystem;
        position = pos;
        rotation = rot;
    }
}