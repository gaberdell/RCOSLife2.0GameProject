using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//This class is for an anvil
public class Anvil : InventoryHolder, IInteractable
{
    public Sprite anvilSprite;
    public GameObject DynText;
    public GameObject spriteChild;
    private SpriteRenderer localRenderer;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    private string ourID;
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EventManager.onGameLoaded += LoadInventory;
    }

    private void OnDisable()
    {
        EventManager.onGameLoaded -= LoadInventory;
    }


    // Start is called before the first frame update
    void Start()
    {
        EventManager.GetID(gameObject, ref ourID);

        SavableSlot[] anvilSaveData = primaryInvSystem.ToSavabaleSlots();

        EventManager.SoftSave(ourID, anvilSaveData);
        localRenderer = spriteChild.GetComponent<SpriteRenderer>();
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.savedSlots.TryGetValue(ourID, out SavableSlot[] chestSlots))
        {
            //Inherits these from InventoryHolder
            primaryInvSystem.WriteInSavableSlots(chestSlots);
        }
    }

    public void Interact(out bool interactSuccessful)
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
