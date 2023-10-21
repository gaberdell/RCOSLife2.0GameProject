using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* class represent the slots in the player's backpack and any other openable items */
/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */
public class DynamicInventoryDisplay : InventoryDisplay
{
    // for other class to access - Maybe a merchant (will use dynamic inventory display) have other
    // function for the inventory that the chest don't but merchant still want to access this variable
    [SerializeField] protected InventorySlot_UI slotPrefab;
    public GameObject dynamicText;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToShow.InventorySlots[i]);
            uiSlot.Init(inventoryToShow.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
        dynamicText.GetComponent<DynamicTextControl>().GrabCorners();
    }

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

    private void OnDisable()
    {
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotChanged -= UpdateSlot;
        }
    }
}