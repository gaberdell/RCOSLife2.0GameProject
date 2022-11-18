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


    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInvSystem);
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
        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInvSystem = data.playerInventory.InvSystem;
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
            OnDynamicInventoryDisplayRequested?.Invoke(primaryInvSystem, offset);
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
