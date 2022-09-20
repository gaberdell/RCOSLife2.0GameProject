using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> Inventory;


    //Inventory getter
    public List<InventorySlot> InventorySlots => Inventory;
    public int InventorySize => InventorySlots.Count;


    //event that activate when we add an item into our inventory 
    public UnityAction<InventorySlot> OnInventorySlotChanged;
    /* Constructor */
    public InventorySystem(int size)
    {
        Inventory = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            Inventory.Add(new InventorySlot());
        }


    }


    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        //if item exist in inventory
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            //add item in and change inventory
            foreach (var slot in invSlot)
            {
                //check to see if there are still room left in the slot
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        
        
        if(HasFreeSlot(out InventorySlot freeSlot)) //Gets the first available slot
        {
            //add item into available slot
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        //check all inventory slot and create a list
        // i is just any item and Where() is just finding where is 
        //item i located at and put it in a list
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true; //if there are still inventory slot, return true, else return false
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

}
