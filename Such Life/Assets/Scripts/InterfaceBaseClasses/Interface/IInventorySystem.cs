using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySystem
{
    public void OnInventorySlotChanged(IInventorySystem inventorySystem, IInventorySlot inventorySlot);
    public int InventorySize { get; set; }
    public List<IInventorySlot> InventorySlots { get; set; }
}