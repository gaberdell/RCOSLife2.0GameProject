using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with slight modification */


[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{
    public GameObject DynTextObject;
    public GameObject spriteChild;
    private SpriteRenderer localRenderer;
    public Sprite[] chestSprites;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private bool isOpen = false;
    private Text DynText;

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.onLoadGame += LoadInventory;
    }

    private void OnEnable()
    {
        EventManager.closeInventoryUIEvent += CloseChest;
    }

    private void OnDisable()
    {
        EventManager.closeInventoryUIEvent -= CloseChest;
    }

    private void Start()
    {
        var chestSaveData = new InventorySaveData(primaryInvSystem, transform.position, transform.rotation);
        SaveGameManager.data.chestDictionaryData.Add(EventManager.GetID(gameObject), chestSaveData);
        localRenderer = spriteChild.GetComponent<SpriteRenderer>();

        DynText = DynTextObject.GetComponent<Text>();
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for this specific chest inventory, and if its exist, load it
        //Maybe add in the chest manager of some sort to load all of the chest in the chest dictionary into the world 
        if (data.chestDictionaryData.TryGetValue(EventManager.GetID(gameObject), out InventorySaveData chestData))
        {
            //Inherits these from InventoryHolder
            primaryInvSystem = chestData.InvSystem;
            transform.position = chestData.Position;
            transform.rotation = chestData.Rotation;
        }
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        // if any is listening out on this event (hence the ?), if yes, invoke it
        if (isOpen == false)
        {
            OnDynamicInventoryDisplayRequested?.Invoke(primaryInvSystem, 0);

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