using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    /* Constructor */
    public InventorySlot(InventoryItemData itemInfo, int amount)
    {
        itemData = itemInfo;
        stackSize = amount;
    }

    //empty out the slot (i.e. delete an item)
    public InventorySlot()
    {
        ClearSlot();
    }




    /* Methods */
    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        
        if(itemData == invSlot.ItemData)
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    //check to see if there is room left in stack, if yes, combine until
    //MaxStackSize and then leave what is left to the other stack
    //similar to minecraft stacking mechanics
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    //amountToAdd == # of different object to Add
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }
}
