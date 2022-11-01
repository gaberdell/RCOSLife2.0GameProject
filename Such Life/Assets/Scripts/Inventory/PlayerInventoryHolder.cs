using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInvSize;
    [SerializeField] protected InventorySystem secondaryInvSystem;

    public InventorySystem SecondaryInventorySystem => secondaryInvSystem;
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;
    public playerAction playerControl;

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
        secondaryInvSystem = new InventorySystem(secondaryInvSize);
    }
    // Update is called once per frame
    void Update()
    {
        bool openInventoryKeyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();
        //fix it so the function use playerAction instead of key press on keyboard
        if (openInventoryKeyPressed)
        {
            OnPlayerBackpackDisplayRequested?.Invoke(secondaryInvSystem);
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        // check to see if the item has been successfully added to the player backpack
        if (primaryInvSystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if (secondaryInvSystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
