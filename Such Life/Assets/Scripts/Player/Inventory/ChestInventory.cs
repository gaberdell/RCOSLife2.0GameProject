using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */
/// <summary>
/// Class <c>ChestInventorya</c> Creates a basic chest functionality.
/// Relationship status : 
/// <c>InventoryHolder</c> based class
/// <c>IInteractable</c> allows it to be interacted with
/// <c>DynamicInventoryDisplay</c> is what is what renders out the inventory.
/// <c>SavableSlot</c> used to save data 
/// </summary>
public class ChestInventory : InventoryHolder, IInteractable
{
    public GameObject DynTextObject;
    public GameObject spriteChild;
    private SpriteRenderer localRenderer;
    public Sprite[] chestSprites;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private string ourID;

    private bool isOpen = false;
    private Text DynText;

    //Is this nesscary?
    protected override void Awake()
    {
        base.Awake();
        
    }

    private void OnEnable()
    {
        EventManager.onGameLoaded += LoadInventory;
        EventManager.closeInventoryUIEvent += CloseChest;
    }

    private void OnDisable()
    {
        EventManager.onGameLoaded -= LoadInventory;
        EventManager.closeInventoryUIEvent -= CloseChest;
    }

    private void Start()
    {
        localRenderer = spriteChild.GetComponent<SpriteRenderer>();
        EventManager.GetID(gameObject, ref ourID);
        DynText = DynTextObject.GetComponent<Text>();

        SavableSlot[] chestSaveData = primaryInvSystem.ToSavabaleSlots();

        EventManager.SoftSave(ourID, chestSaveData);
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for this specific chest inventory, and if its exist, load it
        //Maybe add in the chest manager of some sort to load all of the chest in the chest dictionary into the world 
        if (data.savedSlots.TryGetValue(ourID, out SavableSlot[] chestSlots))
        {
            //Inherits these from InventoryHolder
            primaryInvSystem.WriteInSavableSlots(chestSlots);
        }
    }

    public void Interact(out bool interactSuccessful)
    {
        // if any is listening out on this event (hence the ?), if yes, invoke it
        if (isOpen == false)
        {
            OnDynamicInventoryDisplayRequested?.Invoke(primaryInvSystem, 0);
            EventManager.GetWeaponBag(transform.position);
            Destroy(gameObject);
            DynTextObject.SetActive(true);
            //Name is also inherited
            DynText.text = name;
            isOpen = true;
        }
        else
        {
            EventManager.CloseInventoryUI(false);
        }
        interactSuccessful = true;
    }


    private void CloseChest(bool _)
    {
        isOpen = false;
    }

    //this method will be use for later if the player interact with the chest, they can't move until
    //they close the chest
    
    public void EndInteraction()
    {

    }

    public void Update()
    {
        if(isOpen)
        {
            localRenderer.sprite = chestSprites[1];
            //Add functionality for Overfilled chest
        }
        else{
            localRenderer.sprite = chestSprites[0];
        }
    }
    
}