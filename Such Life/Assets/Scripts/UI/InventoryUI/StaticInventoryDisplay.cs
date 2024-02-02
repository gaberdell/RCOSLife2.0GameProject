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
        // We already assign something in the inspector
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

    protected override void Start()
    {
        base.Start();
        RefreshStaticDisplay();
    }

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
