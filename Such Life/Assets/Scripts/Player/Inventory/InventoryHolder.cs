using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/* This script is for building the user's backpack and chest system */
[System.Serializable] //So it can be seen in the inspector
public abstract class InventoryHolder : MonoBehaviour, IInventoryHolder
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInvSystem;
    //want to increase or decrease size of player hotbar? change this number here
    [SerializeField] protected int offset = 10;

    public int Offset => offset;


    /* Getter */
    public InventorySystem PrimaryInventorySystem => primaryInvSystem;

    //inventory system to display, amount to offset display by
    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;


    protected virtual void Awake()
    {
        //primaryInvSystem.PrintSlots();
        EventManager.onGameLoaded += LoadInventory;

        if (primaryInvSystem == null)
        {
            primaryInvSystem = new InventorySystem(inventorySize);
        }
        else
        {
            primaryInvSystem.AddBlankSlotsToSize(inventorySize);
        }
    }

    public bool AddToPrimaryInventory(InventoryItemData inventoryItemData, int amount)
    {
        return primaryInvSystem.AddToInventory(inventoryItemData, amount);
    }


    protected abstract void LoadInventory(SaveData saveData);
}