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
    [SerializeField] protected InventorySystem inventorySystem;

    /* Getter */
    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;


    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize); 
    }




}   
