using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* This abstract class represent the slots in the player's inventory */
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;    

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    //getter
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        // Look through every slot in the dictionary
        // key: InventorySlot_UI
        // value: InventorySlot
        foreach (var slot in slotDictionary)
        {
            // if the value of the entry is equal to the slot that we pass in
            if(slot.Value == updatedSlot)
            {
                // update the UI correspond to the inventory
                // Slot key - UI representation of the value
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    //This function will incharge of picking up and placing item in the Hotbar
    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        //         If clicked slot have item and                             mouse is not holding an item
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            //If player is holding shift key? Split the Stack from the mouse

            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        // If clicked slot don't have item and mouse is holding an item, then place down the item
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        // If clicked slot has an item and mouse is holding an item, decide what to do?
        //Are both item the same?
        //Yes: Combine the item
        // If slot total item + mouse total item <= stackSize, combine those 2
        // If slot total item + mouse total item > stackSize, combine unil = to stackSize of the item on the slot and leave the rest on the mouse
        //No: Swap item of the slot and the mouse; 
    }
}
