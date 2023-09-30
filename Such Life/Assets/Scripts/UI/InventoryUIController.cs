using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

public class InventoryUIController : MonoBehaviour
{

    [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel;
    public GameObject dynamicText;
    public DynamicInventoryDisplay playerBackpackPanel;

    public playerAction playerControl;
    

    private void Awake()
    {
        playerControl = new playerAction();
        dynamicText.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerControl.Enable();
        InventoryHolder.OnDynamicInventoryDisplayRequested += ShowInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += ShowPlayerInventory;
        EventManager.closeInventoryUIEvent += CloseInventory;
    }

    private void OnDisable()
    {
        playerControl.Disable();
        InventoryHolder.OnDynamicInventoryDisplayRequested -= ShowInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= ShowPlayerInventory;
        EventManager.closeInventoryUIEvent -= CloseInventory;
    }


    // Change it so that the player can use mouse cursor to open up the chest if they are directly in front of them using the interact button or right click 
    // To open player's backpack, they have to press tab and to close it, either press tab or escape button
    
    public void CloseInventory(bool closePlayerInventory)
    {
        //check to see if you press the key or not
        

        if (inventoryPanel.gameObject.activeInHierarchy)
        {
            dynamicText.SetActive(false);
            inventoryPanel.gameObject.SetActive(false);
        }

        if (playerBackpackPanel.gameObject.activeInHierarchy && closePlayerInventory)
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        bool closeInventoryKeyPressed = playerControl.Player.CloseInventory.WasPressedThisFrame();
        if (closeInventoryKeyPressed) {
            EventManager.CloseInventoryUI(true);
        }
           
    }
    
    void ShowInventory(InventorySystem inventoryToShow, int offset)
    {
        inventoryPanel.RefreshDynamicInventory(inventoryToShow, offset);
        inventoryPanel.gameObject.SetActive(true);
    }


    void ShowPlayerInventory(InventorySystem inventoryToShow, int offset)
    {
        playerBackpackPanel.RefreshDynamicInventory(inventoryToShow, offset);
        playerBackpackPanel.gameObject.SetActive(true);
    }


}
