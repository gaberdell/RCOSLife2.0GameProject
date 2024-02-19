using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/* This script is for building the user's backpack and chest system */
/// <summary>
/// Class <c>InventoryHolder</c> Base class for PlayerInventoryHolder
///     - Creates abstract class that makes interaction with an InvetorySystem easier
///       can be seen in player classes,
///     - When the internal system is updated it tells the hotbar to update
///     - BUT ALSO when the player inventory opens it updates player's hotbar???
/// Relationship status : 
/// <c>MonoBehaviour</c> based class
/// <c>IInventoryHolder</c> has been implemented
/// Implements the AddToPrimaryInventory which interfaces with add to inventory
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
        EventManager.onGameLoaded += LoadInventory;

        primaryInvSystem = new InventorySystem(inventorySize); 
    }

    public bool AddToPrimaryInventory(InventoryItemData inventoryItemData, int amount)
    {
        return primaryInvSystem.AddToInventory(inventoryItemData, amount);
    }


    protected abstract void LoadInventory(SaveData saveData);
}