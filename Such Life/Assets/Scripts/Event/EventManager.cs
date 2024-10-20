using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Make the Delegate
    public delegate void CloseInventoryDelegate(bool closePlayerInventory);

    public delegate void UpdateInventorySlotDelegate(IInventorySystem inventorySystem, IInventorySlot inventorySlot);

    public delegate void RemoveSpecificDataDelegate(string ID, bool isSavableObjects);
    public delegate void SoftSaveDataDelegate(string ID, SavableSlot[] slots);
    public delegate void SoftSaveItemDelegate(string ID, ItemPickUpSaveData slots);
    public delegate void OnDataLoadedDelegate(SaveData saveData);
    public delegate void OnRemoveDataDelegate();
    public delegate void ForceIDValidiationDelegate(GameObject gameObjectWithID);

    public delegate void SetPlayerHealthBarDelegate(float health);

    public delegate void SetPlayerWalkSpeedDelegate(float speed);

    public delegate void InventorySlotPressed(GameObject inventoryslot);

    public delegate void CurrentItemSelected(InventoryItemData selectedItem);

    public delegate void CurrentSlotSelected(IInventorySlot selectedItem);

    //Delegates with returns

    public delegate bool ContainSpecificDataDelegate(string ID, bool isSavableObjects);
    public delegate bool OnSaveGameDelegate(SaveData saveData);
    public delegate SaveData OnLoadGameDelegate();
    public delegate string GetIDDelegate(GameObject gameObjectWithID, ref string id);

    public delegate bool DealDamageDelegate(GameObject objectToDealDamageTo, float damageAmount);

    public delegate float GetWalkSpeedDelegate();

    public delegate void GetWeaponBagDelegate(Vector3 postition);

    //Make the Event
    public static event CloseInventoryDelegate closeInventoryUIEvent;

    public static event UpdateInventorySlotDelegate updateInventorySlot;

    public static event ContainSpecificDataDelegate onContainSpecificData;
    public static event RemoveSpecificDataDelegate onRemoveSpecificData;
    public static event SoftSaveDataDelegate onSoftSaveData;
    public static event SoftSaveItemDelegate onSoftSaveItem;
    public static event OnDataLoadedDelegate onGameLoaded;
    public static event OnSaveGameDelegate onSaveGame;
    public static event OnRemoveDataDelegate onDeleteData;
    public static event OnLoadGameDelegate onLoadGame;
    public static event ForceIDValidiationDelegate forceIDValidation;

    public static event SetPlayerHealthBarDelegate setPlayerHealthBar;

    public static event SetPlayerWalkSpeedDelegate setPlayerWalkSpeed;

    public static event InventorySlotPressed inventorySlotPressed;

    public static event CurrentItemSelected currentlySelectedItem;

    public static event CurrentSlotSelected currentSlotSelected;

        //Events with returns

    public static event GetIDDelegate getID;

    public static event DealDamageDelegate onDealDamage;

    public static event GetWalkSpeedDelegate getWalkSpeed;

    public static event GetWeaponBagDelegate getWeaponBag;

    


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
        if (setPlayerHealthBar != null) setPlayerHealthBar(health); 
    }

    public static void SetPlayerWalkSpeed(float speed)
    {
        if (setPlayerWalkSpeed != null) setPlayerWalkSpeed(speed);
    }

    public static void GameLoaded(SaveData loadedData)
    {
        if (onGameLoaded != null) onGameLoaded(loadedData);
    }

    public static void SoftSave(string ID, SavableSlot[] slots)
    {
        if (onSoftSaveData != null) onSoftSaveData(ID, slots);
    }

    public static void SoftSaveItem(string ID, ItemPickUpSaveData item)
    {
        if (onSoftSaveItem != null) onSoftSaveItem(ID, item);
    }

    public static void RemoveSpecificData(string ID, bool isObjectsToSave = false)
    {
        if (onRemoveSpecificData != null) onRemoveSpecificData(ID, isObjectsToSave);
    }

    public static void ForceIDValidation(GameObject gameObjectWithID)
    {
        if (forceIDValidation != null) forceIDValidation(gameObjectWithID);
    }

    public static void DeleteData() { if (onDeleteData != null) onDeleteData(); }

    public static void PressInventorySlot(GameObject inventoryslot)
    {
        if (inventorySlotPressed != null) inventorySlotPressed(inventoryslot);
    }

    public static void SelectedCurrentItem(InventoryItemData selectedItem)
    {
        if (currentlySelectedItem != null) currentlySelectedItem(selectedItem);
    }

    public static void SelectedCurrentSlot(IInventorySlot selectedSlot)
    {
        if (currentSlotSelected != null) currentSlotSelected(selectedSlot);
    }

    public static bool ContainSpecificData(string ID, bool isObjectsToSave = false)
    {
        if (onContainSpecificData != null) return onContainSpecificData(ID, isObjectsToSave);
        else return false;
    }
    public static bool SaveGame(SaveData saveData)
    {
        if (onSaveGame != null) return onSaveGame(saveData);
        else return false;
    }
    public static SaveData LoadData()
    {
        if (onLoadGame != null) return onLoadGame();
        else return null;
    }

    public static string GetID(GameObject gameObjectWithID, ref string id)
    {
        if (getID != null) return getID(gameObjectWithID, ref id);
        else return null;
    }

    public static bool DealDamage(GameObject objectToDealDamageTo, float damageAmount)
    {
        if (onDealDamage != null) return onDealDamage(objectToDealDamageTo, damageAmount);
        else return false;
    }

    public static float GetPlayerWalkSpeed()
    {
        if (getWalkSpeed != null) return getWalkSpeed();
        else return 0f;
    }

    public static void GetWeaponBag(Vector3 position)
    {
        if(getWeaponBag != null) getWeaponBag(position);
    }
}
