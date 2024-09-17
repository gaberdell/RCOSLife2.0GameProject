using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */
/// <summary>
/// Class <c>PlayerInventoryHolder</c> Has DUAL function
///     - Holds the data for the player's inventory
///       this includes items and such using InventoryHolders
///       PrimaryInventorySystem member that holds an InventorySystem
///     - When the internal system is updated it tells the hotbar to update
///     - BUT ALSO when the player inventory opens it updates player's hotbar???
/// Relationship status : 
/// <c>MonoBehaviour</c> based class
/// <c>InventoryDisplay</c> what it mainly interfaces with.
/// Specifically <c>StaticInventoryDisplay</c> to work witht he hotbar
/// <c>InventoryHolder</c> is the parent class which inherits also
/// From <c>MonoBehaviour</c> but also <c>IInventoryHolder</c>
/// Which is used to get to the Save System.
/// </summary>
public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;
    public playerAction playerControl;
    private string playerChestID;
    //want to increase or decrease size of player hotbar? change this number here
    [SerializeField] protected int armorOffset = 8;

    public int ArmorOffset => armorOffset;

    public static UnityAction<InventorySystem, int, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        EventManager.GetID(gameObject, ref playerChestID);
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
        if (openInventoryKeyPressed)
        {
            OnPlayerInventoryDisplayRequested?.Invoke(primaryInvSystem, offset, armorOffset);
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
