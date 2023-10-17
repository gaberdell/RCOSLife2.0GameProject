using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//This class is for an anvil
[RequireComponent(typeof(UniqueID))]
public class Anvil : InventoryHolder, IInteractable
{
    public Sprite anvilSprite;
    public GameObject DynText;
    public GameObject spriteChild;
    private SpriteRenderer localRenderer;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        SaveLoad.onLoadGame += LoadInventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        var anvilSaveData = new InventorySaveData(primaryInvSystem, transform.position, transform.rotation);
        SaveGameManager.data.anvilDictionaryData.Add(GetComponent<UniqueID>().ID, anvilSaveData);
        localRenderer = spriteChild.GetComponent<SpriteRenderer>();
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.anvilDictionaryData.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData anvilData))
        {
            this.primaryInvSystem = anvilData.InvSystem;
            this.transform.position = anvilData.Position;
            this.transform.rotation = anvilData.Rotation;
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

    public void EndInteraction()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
