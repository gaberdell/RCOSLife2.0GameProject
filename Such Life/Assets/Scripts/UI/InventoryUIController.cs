using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void OpenInventory()
    {
        //check to see if you press the key or not

        //(Fix this so that the open inventory only call when the key is pressed and release)
        bool keyPressed = playerControl.Player.OpenInventory.WasPressedThisFrame();


        if (keyPressed && !inventoryPanel.gameObject.activeInHierarchy)
        {
            ShowInventory(new InventorySystem(30));
        }
        else if(keyPressed && inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
    }

    void ShowInventory(InventorySystem inventoryToShow)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventoryToShow);
    }
}
