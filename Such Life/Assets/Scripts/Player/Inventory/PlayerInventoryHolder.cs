using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;
    public playerAction playerControl;
    private string playerChestID;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        EventManager.GetID(gameObject, ref playerChestID);
        Debug.Log(playerChestID);
        EventManager.SoftSave(playerChestID, primaryInvSystem.ToSavabaleSlots());
    }


    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }
    protected override void Awake()
    {
        base.Awake();
        playerControl = new playerAction();
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for player's inventory
        if (data.savedSlots.TryGetValue(playerChestID, out SavableSlot[] inventorySlots))
        {
            primaryInvSystem.WriteInSavableSlots(inventorySlots);
            OnPlayerInventoryChanged?.Invoke();
        }
    }


    // Update is called once per frame
    void Update()
    {
        bool openInventoryKeyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();
        //fix it so the function use playerAction instead of key press on keyboard
        if (openInventoryKeyPressed)
        {
            OnPlayerInventoryDisplayRequested?.Invoke(primaryInvSystem, offset);
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        // check to see if the item has been successfully added to the player backpack
        if (primaryInvSystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
