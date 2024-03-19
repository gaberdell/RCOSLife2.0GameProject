using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

/// <summary>
/// Class <c>InventoryUIController</c> Type of overseer class? Seems to 
///                                    relay the fact that an inventory was 
///                                    opened to the two 
///                                    DynamicInvetoryDisplay each 
///                                    representing a different display.
///                                    Additionally it controls the displayer
///                                    being able to be seen by the player.
///                                    
/// Relationship status : 
/// <c>MonoBehaviour</c> based class.
/// <c>playerAction</c> Gets the input from this class
/// <c>DynamicInventoryDisplay</c> Displays the players version when button
///                                is pressed by player and also controls
///                                the chest inventory inside.
///                                
/// </summary>
public class InventoryUIController : MonoBehaviour
{

    [FormerlySerializedAs("chestPanel")] 
    public DynamicInventoryDisplay inventoryPanel;
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
        //Grab the static event held within inventory holder and use it whenever inventory is shown
        //kinda sus tbh
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
        Debug.Log(inventoryToShow);
        playerBackpackPanel.RefreshDynamicInventory(inventoryToShow, offset);
        playerBackpackPanel.gameObject.SetActive(true);
    }


}
