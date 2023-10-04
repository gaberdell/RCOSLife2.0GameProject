using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

[System.Serializable] //So can be seen in the inspector

public class InventorySlot : IInventorySlot
{
    [SerializeField] private InventoryItemData itemData; //Reference to the item
    [SerializeField] private int stackSize; // current stack size of an item

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

    // Assign item directly to the slot
    public void AssignItem(InventorySlot invSlot)
    {
        //Does the slot contain same item? Yes, add to the stack. No, assign new item to the slot
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

    // Directly update the slot instead of replace the item.
    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    //check to see if there is room left in stack, if yes, combine until
    //MaxStackSize and then leave what is left to the other stack
    //similar to minecraft stacking mechanics
    public bool IsEnoughSpaceInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.MaxStackSize - stackSize;
        return IsEnoughSpaceInStack(amountToAdd);
    }

    //amountToAdd == # of different object to Add
    public bool IsEnoughSpaceInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        //enough to be able to split stack size therefore number of item have be more than 1 
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        //calculate half of the stack
        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); //create copy of the slot w/ 1/2 stack size
        return true;
    }
}
