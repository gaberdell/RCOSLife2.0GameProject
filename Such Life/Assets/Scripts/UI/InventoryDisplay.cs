using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


/* This abstract class represent the slots in the player's inventory */
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] PlayerItemData playerInventoryItem;    

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

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot clicked");
    }
}
