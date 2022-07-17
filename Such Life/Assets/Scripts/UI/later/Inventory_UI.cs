using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Inventory_UI : MonoBehaviour
{   
    public GameObject InventroyScreen;
    PlayerControl playerControl;

    /*
    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    
    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    //get whatever input it being send in by the InputSystem  
    public void OpenInventory()
    {
        //check to see if you press the key or not
        bool keyPressed = playerControl.Player.OpenInventory.ReadValue<float>() >= 0.6f;
        if (keyPressed)
        {
            ShowInventory();
        }
    }
    */

    //show the inventory to the player
    public void ShowInventory()
    {
        //turn "on" and "off" the inventory game Object
        if (!InventroyScreen.activeInHierarchy)
        {
            InventroyScreen.SetActive(true);
        }
        else
        {
            InventroyScreen.SetActive(false);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
