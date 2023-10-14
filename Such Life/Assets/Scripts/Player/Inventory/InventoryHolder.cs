using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/* This script is for building the user's backpack and chest system */
[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
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

    protected abstract void LoadInventory(SaveData saveData);
}