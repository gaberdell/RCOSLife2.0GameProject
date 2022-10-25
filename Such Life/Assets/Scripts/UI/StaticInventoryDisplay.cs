using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

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
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(slots.Length != inventorySystem.InventorySize)
        {
            Debug.Log($"Inventory slot out of sync on this {this.gameObject}");
        }

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);  
        }
    }

    
}
