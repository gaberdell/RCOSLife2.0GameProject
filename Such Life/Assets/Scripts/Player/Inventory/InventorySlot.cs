using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

[System.Serializable] //So can be seen in the inspector
/// <summary>
/// Class <c>InventorySlot</c> job is to manage a singular piece of Inventory Item Data alongside its stack size.
/// Relationship status : 
/// <c>IInventorySlot</c> based on this interface for connectivity purposes
/// <c>InventoryItemData</c> is what holds most of its data
/// Has functionality for clear slots, adding to slots, removing from stack
/// adding, assining, updating, and finding if there is enough space in the slot
/// <c>InventorySlot_UI</c> goes ahead and calls the ClearSlot();
/// </summary>
public class InventorySlot : IInventorySlot
{
    [SerializeField] private InventoryItemData itemData; //Reference to the item
    [SerializeField] private int stackSize; // current stack size of an item
    private ItemType itemTypeCheck;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public ItemType ItemTypeCheck { get { return itemTypeCheck; } set { itemTypeCheck = value; } }

    public delegate void ItemSlotUpdateDelegate();

    public event ItemSlotUpdateDelegate itemSlotUpdated;


    private void fireItemSlotUpdate()
    {
        if (stackSize <= 0)
        {
            stackSize = -1;
            itemData = null;
            itemTypeCheck = 0;
        }
        if (itemSlotUpdated != null)
        {
            itemSlotUpdated();
        }
    }

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
        fireItemSlotUpdate();
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        fireItemSlotUpdate();
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
        fireItemSlotUpdate();
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


    //
}
