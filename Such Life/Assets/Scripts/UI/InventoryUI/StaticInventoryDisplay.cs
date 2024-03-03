using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/// <summary>
/// Used for the hotbar
/// </summary>
public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;


    private void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay; 
    }

    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }

    private void RefreshStaticDisplay()
    {
        //[NEEDS FIXING]
        // We already assign something in the inspector
        // Memory Leak here if we are telling inventory to repeatedly
        // Listen to this event a bunch of times then it doesn't get
        // Decalled?! why is it written this way?!
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }

        AssignSlot(inventorySystem, 0);
    }

    void Start()
    {
        RefreshStaticDisplay();
    }

    //[POTENTIALLY] [NEEDS FIXING]
    //This could be a PROBLEM not passed in as a reference just passed in
    //This could not even be effecting the original then!!
    //Would explain in DynamicInventoryDisplay why the original isn't updated
    //I hope this is actually referencing something on the heap not just
    //Well an abstract type.. idk
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        for (int i = 0; i < inventoryHolder.Offset; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);  
        }
    }

    
}
