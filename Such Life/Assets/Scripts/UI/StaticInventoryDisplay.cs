using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots; 

    protected override void Start()

    {
        base.Start();

        // We already assign something in the inspector
        if(inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        throw new System.NotImplementedException();
    }

    
}
