using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public playerAction playerControl;

    //Key press action
    InputAction openInventory = new InputAction("Open Inventory");
    

    private void Awake()
    {
        playerControl = new playerAction();
        inventoryPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerControl.Enable();
        InventoryHolder.OnDynamicInventoryDisplayRequested += ShowInventory;
    }

    private void OnDisable()
    {
        playerControl.Disable();
        InventoryHolder.OnDynamicInventoryDisplayRequested -= ShowInventory;
    }

    /*
    public void OpenInventory()
    {
        //check to see if you press the key or not

        bool openInventoryKeyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();
        bool closeInventoryKeyPressed = playerControl.Player.CloseInventory.WasPressedThisFrame();

        if ((openInventoryKeyPressed || closeInventoryKeyPressed) && inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }


        /* 
        if (openInventoryKeyPressed && !inventoryPanel.gameObject.activeInHierarchy)
        {
            ShowInventory(new InventorySystem(30));
        }
        else if((openInventoryKeyPressed || closeInventoryKeyPressed) && inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        
    }
    */


    // Update is called once per frame
    void Update()
    {
        bool openInventoryKeyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();
        bool closeInventoryKeyPressed = playerControl.Player.CloseInventory.WasPressedThisFrame();

        if ((openInventoryKeyPressed || closeInventoryKeyPressed) && inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        //OpenInventory();
    }

    void ShowInventory(InventorySystem inventoryToShow)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventoryToShow);
    }
}
