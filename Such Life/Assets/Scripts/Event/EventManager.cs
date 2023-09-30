using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Make the Delegate
    public delegate void CloseInventoryDelegate(bool closePlayerInventory);

    
    //Make the Event
    public static event CloseInventoryDelegate closeInventoryUIEvent;

    //Make it so the event can be called
    public static void CloseInventoryUI(bool closePlayerInventory) {
        if (closeInventoryUIEvent != null) closeInventoryUIEvent(closePlayerInventory);
    }
}
