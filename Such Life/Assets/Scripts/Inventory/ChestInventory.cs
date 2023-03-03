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
    public GameObject DynText;
    public GameObject spriteChild;
    private SpriteRenderer localRenderer;
    public Sprite[] chestSprites;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    protected override void Awake()
    {
        base.Awake();
        SaveLoad.onLoadGame += LoadInventory;
    }

    private void Start()
    {
        var chestSaveData = new InventorySaveData(primaryInvSystem, transform.position, transform.rotation);
        SaveGameManager.data.chestDictionaryData.Add(GetComponent<UniqueID>().ID, chestSaveData);
        localRenderer = spriteChild.GetComponent<SpriteRenderer>();
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for this specific chest inventory, and if its exist, load it
        //Maybe add in the chest manager of some sort to load all of the chest in the chest dictionary into the world 
        if (data.chestDictionaryData.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInvSystem = chestData.InvSystem;
            this.transform.position = chestData.Position;
            this.transform.rotation = chestData.Rotation;
        }
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        // if any is listening out on this event (hence the ?), if yes, invoke it
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInvSystem, 0);
        DynText.SetActive(false);
        DynText.SetActive(true);
        DynText.GetComponent<Text>().text = this.name.ToString();
        interactSuccessful = true;
    }

    //this method will be use for later if the player interact with the chest, they can't move until
    //they close the chest
    
    public void EndInteraction()
    {

    }

    public void Update()
    {
        if(DynText.activeInHierarchy && DynText.GetComponent<Text>().text == this.name.ToString()){
            localRenderer.sprite = chestSprites[1];
            //Add functionality for Overfilled chest
        }
        else{
            localRenderer.sprite = chestSprites[0];
        }
    }
    
}