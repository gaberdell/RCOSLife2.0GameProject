using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Make the Delegate
    public delegate void CloseInventoryDelegate(bool closePlayerInventory);

    public delegate void UpdateInventorySlotDelegate(IInventorySystem inventorySystem, IInventorySlot inventorySlot);

    //Make the Event
    public static event CloseInventoryDelegate closeInventoryUIEvent;

    public static event UpdateInventorySlotDelegate updateInventorySlot;

    //Make it so the event can be called
    public static void CloseInventoryUI(bool closePlayerInventory) {
        if (closeInventoryUIEvent != null) closeInventoryUIEvent(closePlayerInventory);
    }

    public static void UpdateInventorySlot(IInventorySystem inventorySystem, IInventorySlot inventorySlot)
    {
        if (updateInventorySlot != null) updateInventorySlot(inventorySystem, inventorySlot);
    }
}
