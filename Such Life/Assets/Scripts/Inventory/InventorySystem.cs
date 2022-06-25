using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlots> Inventory;


    //Inventory getter
    public List<InventorySlots> InventorySlots => Inventory;
    public int InventorySize => InventorySlots.Count;


    //event that activate when we add an item into our inventory 
    public UnityAction<InventorySlots> OnInventorySlotChanged;
    /* Constructor */
    public InventorySystem(int size)
    {
        Inventory = new List<InventorySlots>(size);

        for (int i = 0; i < size; i++)
        {
            Inventory.Add(new InventorySlots());
        }


    }



    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        Inventory[0] = new InventorySlots(itemToAdd, amountToAdd);
        return true;
    }





}
