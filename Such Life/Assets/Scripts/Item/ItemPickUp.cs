using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUpItem();

    }


    void PickUpItem()
    {
        Debug.Log("Pick up" + item.name);
        //Add item into inventory (Destroy game object in the current scene and add that item into the inventory)
        Destroy(gameObject);

    }
    /*
    public float pickUpRadius = 1f;
    public InventoryItemData ItemData; // would changing this to a general var/ scriptable object data type work? ItemData can be a piece of armor, equipment, building materials, resources, ...
    
    private CircleCollider2D myCollider;
    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    // items can only be picked up if it's 2s after being created, freezetime set to 2s initially
    public float freezetime = 2f;
    private float freezecount;
    public bool freeze = false;

    private void Awake()
    {
        
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);
        
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;

        freeze = true;
        freezecount = freezetime;
    }

    private void OnEnable()
    {

        EventManager.onGameLoaded += LoadGame;
    }

    private void OnDisable()
    {
        EventManager.onGameLoaded -= LoadGame;
    }

    private void LoadGame(SaveData data)
    {
        if (data.savedSlots.ContainsKey(id))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (EventManager.ContainSpecificData(id, true))
        {
            EventManager.RemoveSpecificData(id, true);
        }
    }

    private void Start()
    {
        EventManager.GetID(gameObject, ref id);
        EventManager.SoftSaveItem(id, itemSaveData);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        //adjust this function slightly when start to implement player and chest inventory
        var inventory = other.transform.GetComponent<IInventoryHolder>();
        string inventoryID = null;
        EventManager.GetID(other.gameObject, ref inventoryID);

        if ((inventory == null) || (freeze)) return;

        if (inventory.AddToPrimaryInventory(ItemData, 1))
        { 
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        freezecount -= Time.deltaTime;
        if (freezecount <= 0f && freeze)
        {
            freeze = false;
        }
    }
    */
}
