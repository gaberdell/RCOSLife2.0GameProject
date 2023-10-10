using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Make the Delegate
    public delegate void CloseInventoryDelegate(bool closePlayerInventory);

    public delegate void UpdateInventorySlotDelegate(IInventorySystem inventorySystem, IInventorySlot inventorySlot);

    public delegate void OnSaveGameDelegate();
    public delegate void OnRemoveDataDelegate();
    public delegate void ForceIDValidiationDelegate();

    public delegate void SetPlayerHealthBarDelegate(float health);

    //Delegates with returns

    public delegate string GetIDDelegate(GameObject gameObjectWithID);

    public delegate bool DealDamageDelegate(GameObject objectToDealDamageTo, float damageAmount);
    

    //Make the Event
    public static event CloseInventoryDelegate closeInventoryUIEvent;

    public static event UpdateInventorySlotDelegate updateInventorySlot;

    public static event OnSaveGameDelegate onSaveGame;
    public static event OnRemoveDataDelegate onDeleteData;
    public static event ForceIDValidiationDelegate forceIDValidation;

    public static event SetPlayerHealthBarDelegate setPlayerHealthBar;

    public static event GetIDDelegate getID;

    public static event DealDamageDelegate onDealDamage;

    //Make it so the event can be called
    public static void CloseInventoryUI(bool closePlayerInventory) {
        if (closeInventoryUIEvent != null) closeInventoryUIEvent(closePlayerInventory);
    }

    public static void UpdateInventorySlot(IInventorySystem inventorySystem, IInventorySlot inventorySlot)
    {
        if (updateInventorySlot != null) updateInventorySlot(inventorySystem, inventorySlot);
    }

    public static void SetPlayerHealthBar(float health) 
    { 
        if (forceIDValidation != null) setPlayerHealthBar(health); 
    }

    public static void SaveGame() { if (onSaveGame != null) onSaveGame(); }
    public static void DeleteData() { if (onDeleteData != null) onDeleteData(); }
    public static void ForceIDValidation() { if (forceIDValidation != null) forceIDValidation(); }
    

    public static string GetID(GameObject gameObjectWithID)
    {
        if (getID != null) return getID(gameObjectWithID);
        else return null;
    }

    public static bool DealDamage(GameObject objectToDealDamageTo, float damageAmount)
    {
        if (onDealDamage != null) return onDealDamage(objectToDealDamageTo, damageAmount);
        else return false;
    }
}
