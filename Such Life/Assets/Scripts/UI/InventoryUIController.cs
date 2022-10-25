using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    public playerAction playerControl;
    

    private void Awake()
    {
        playerControl = new playerAction();
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerControl.Enable();
        InventoryHolder.OnDynamicInventoryDisplayRequested += ShowInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += ShowPlayerBackpack;
    }

    private void OnDisable()
    {
        playerControl.Disable();
        InventoryHolder.OnDynamicInventoryDisplayRequested -= ShowInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= ShowPlayerBackpack;
    }


    // Change it so that the player can use mouse cursor to open up the chest if they are directly in front of them using the interact button or right click 
    // To open player's backpack, they have to press tab and to close it, either press tab or escape button
    
    public void CloseInventory()
    {
        //check to see if you press the key or not
        //bool openInventoryKeyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();
        bool closeInventoryKeyPressed = playerControl.Player.CloseInventory.WasPressedThisFrame();
        //bool interactingKeyPressed = playerControl.Player.Interacting.WasPressedThisFrame();

        if (closeInventoryKeyPressed && chestPanel.gameObject.activeInHierarchy)
        {
            chestPanel.gameObject.SetActive(false);
        }

        if (closeInventoryKeyPressed && playerBackpackPanel.gameObject.activeInHierarchy)
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        CloseInventory();
    }
    
    void ShowInventory(InventorySystem inventoryToShow)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(inventoryToShow);
    }

    void ShowPlayerBackpack(InventorySystem inventoryToShow)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(inventoryToShow);
    }
}
