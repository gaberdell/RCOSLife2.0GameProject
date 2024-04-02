using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* class represent the slots in the player's backpack and any other openable items */
/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */
/// <summary>
/// Class <c>DynamicInventoryDisplay</c> class that stores and presents a dynamic inventory holder.
///                                      Class has a public method for InventoryUIController
///                                      called Refresh slots used for setting it to show the
///                                      current inventory.
///                                      
/// Relationship status : 
/// <c>InventoryDisplay</c> based class.
/// <c>InventorySlot_UI</c> Uses it to be updated and as a prefab.
/// <c>InventorySlot</c> Uses it to be updated.
/// <c>DynamicTextControl</c> calls this script to update itself despite it already updating itself..
/// <c>InventoryUIController</c> Is the thing that actually starts using the 
///                              public RefreshDynamicInventory method alongside
///                              passing in an InventorySystem to show
/// </summary>
public class DynamicInventoryDisplay : InventoryDisplay
{
    // for other class to access - Maybe a merchant (will use dynamic inventory display) have other
    // function for the inventory that the chest don't but merchant still want to access this variable
    [SerializeField] 
    private InventorySlot_UI slotPrefab;

    public GameObject DynamicText { get; set; }

    //Helper class
    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            //Implement object pooling later to improve performance
            Destroy(item.gameObject);
        }

        if (slotDictionary != null)
        {
            slotDictionary.Clear();
        }
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToShow, int offset)
    {
        //clear out slots, update and assign slots 
        ClearSlots();
        inventorySystem = inventoryToShow;
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }

        AssignSlot(inventoryToShow, offset);
    }

    public override void AssignSlot(InventorySystem inventoryToShow, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        //prevent accessing null object (NullException Error)
        if (inventoryToShow == null)
        {
            return;
        }


        //create and pair the inventory slot
        for (int i = offset; i < inventoryToShow.InventorySize; i++)
        {
            //What the fudge knuckles is this?
            InventorySlot_UI uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToShow.InventorySlots[i]);
            uiSlot.Init(inventoryToShow.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
        //DynamicText.GetComponent<DynamicTextControl>().GrabCorners();
    }

    private void OnDisable()
    {
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotChanged -= UpdateSlot;
        }
    }
}