using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */


/// <summary>
/// Class <c>InventorySystem</c> holds a list of slots for other scripts to instiatiate and interact with.
/// Relationship status : 
/// Mainly interfaces with a list of <c>InventorySlot</c>
/// Uses the LINQ library to make methods for interactoring with the InventorySlots easier
/// </summary>
[System.Serializable]
public class InventorySystem
{
    [SerializeField] 
    private List<InventorySlot> Inventory;
    //public int InventorySize { get; set; }

    //Inventory getter
    public List<InventorySlot> InventorySlots => Inventory;

    public int InventorySize { get => InventorySlots.Count; }
    //public int InventorySize {return InventorySlots.Count; };

    //OLD CODE TO RE WRITE : event that activate when we add an item into our inventory 
    //public UnityAction<InventorySlot> OnInventorySlotChanged;
    //event that activate when we add an item into our inventory 

    /* Constructor */
    //Copy over things intiated in editor for set chests
    public InventorySystem(int size)
    {

        if (Inventory == null)
        {
            Inventory = new List<InventorySlot>(size);
        }

        AddBlankSlotsToSize(size);
    }

    public void AddBlankSlotsToSize(int size)
    {
        for (int i = Inventory.Count; i < size; i++)
        {
            InventorySlot newInventorySlot = new InventorySlot();
            Inventory.Add(newInventorySlot);
        }
    }




    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        //if item exist in inventory
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            //add item in and change inventory
            foreach (var emptySlot in invSlot)
            {
                //check to see if there are still room left in the slot
                if (emptySlot.IsEnoughSpaceInStack(amountToAdd))
                {
                    emptySlot.AddToStack(amountToAdd);
                    //EventManager.UpdateInventorySlot(this, emptySlot);
                    return true;
                }
            }
        }
        
        
        if(HasFreeSlot(out InventorySlot freeSlot)) //Gets the first available slot
        {
            if (freeSlot.IsEnoughSpaceInStack(amountToAdd))
            {
                //add item into available slot
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                //EventManager.UpdateInventorySlot(this, freeSlot);
                return true;
            }
            /* implement such that to only take what can fill the stack and check for another free slot 
            to put the rest of the item in */
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

    public SavableSlot[] ToSavabaleSlots()
    {
        SavableSlot[] returnArray = new SavableSlot[InventorySlots.Count];

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            returnArray[i].amount = InventorySlots[i].StackSize;
            returnArray[i].itemData = InventorySlots[i].ItemData;
        }

        return returnArray;
    }

    public void WriteInSavableSlots(SavableSlot[] savableSlots)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlots[i].UpdateInventorySlot(savableSlots[i].itemData, savableSlots[i].amount);
        }
    }

    public void PrintSlots()
    {
        if (InventorySlots == null)
        {
            Debug.Log("Slots technically null");
        }

        string stringToAddTogther = "";
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].ItemData != null)
            {
                stringToAddTogther += InventorySlots[i].ItemData.DisplayName + ", ";
            }
            else
            {
                stringToAddTogther += "NULL" + ", ";
            }
        }
        Debug.Log(stringToAddTogther);
    }
}


/// <summary>
/// Was a once used Struct to carry Save Data now it goes unused.
/// The 2 references are just the struct referencing itself..
/// </summary>
[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;

    public Vector3 Position;
    public Quaternion Rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 pos, Quaternion rot)
    {
        InvSystem = _invSystem;
        Position = pos;
        Rotation = rot;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        InvSystem = _invSystem;
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
    }
}
