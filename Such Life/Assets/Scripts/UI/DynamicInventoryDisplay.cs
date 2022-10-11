using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* class represent the slots in the player's backpack and any other openable items */
/* Codes provided by: Dan Pos - Game Dev Tutorials! */
public class DynamicInventoryDisplay : InventoryDisplay
{
    // for other class to access - Maybe a merchant (will use dynamic inventory display) have other
    // function for the inventory that the chest don't but merchant still want to access this variable
    [SerializeField] protected InventorySlot_UI slotPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToShow) 
    {
        //clear out slots, update and assign slots 
        ClearSlots();
        inventorySystem = inventoryToShow;
        AssignSlot(inventoryToShow);
    }

    public override void AssignSlot(InventorySystem inventoryToShow)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        //prevent accessing null object (NullException Error)
        if(inventoryToShow == null)
        {
            return;
        }

        //create and pair the inventory slot
        for(int i = 0; i < inventoryToShow.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToShow.InventorySlots[i]);
            uiSlot.Init(inventoryToShow.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>())
        {
            //Implement object pooling later to improve performance
            Destroy(item.gameObject);
        }

        if(slotDictionary != null)
        {
            slotDictionary.Clear();
        }
    }
}