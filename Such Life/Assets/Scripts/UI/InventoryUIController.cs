using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

public class InventoryUIController : MonoBehaviour
{

    [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    public playerAction playerControl;
    

    private void Awake()
    {
        playerControl = new playerAction();
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerControl.Enable();
        InventoryHolder.OnDynamicInventoryDisplayRequested += ShowInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += ShowPlayerInventory;
    }

    private void OnDisable()
    {
        playerControl.Disable();
        InventoryHolder.OnDynamicInventoryDisplayRequested -= ShowInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= ShowPlayerInventory;
    }


    // Change it so that the player can use mouse cursor to open up the chest if they are directly in front of them using the interact button or right click 
    // To open player's backpack, they have to press tab and to close it, either press tab or escape button
    
    public void CloseInventory()
    {
        //check to see if you press the key or not
        bool closeInventoryKeyPressed = playerControl.Player.CloseInventory.WasPressedThisFrame();

        if (closeInventoryKeyPressed && inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
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
    
    void ShowInventory(InventorySystem inventoryToShow, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventoryToShow, offset);
    }

    void ShowPlayerInventory(InventorySystem inventoryToShow, int offset)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(inventoryToShow, offset);
    }
}
