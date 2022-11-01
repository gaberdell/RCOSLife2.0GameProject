using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/* This script is for building the user's backpack and chest system */
[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInvSystem;

    /* Getter */
    public InventorySystem PrimaryInventorySystem => primaryInvSystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;


    protected virtual void Awake()
    {
        primaryInvSystem = new InventorySystem(inventorySize); 
    }
}   
