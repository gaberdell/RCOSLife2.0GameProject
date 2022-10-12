using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickUp : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public InventoryItemData ItemData;

    private CircleCollider2D myCollider;

    // items can only be picked up if it's 2s after being created, freezetime set to 2s initially
    public float freezetime = 2f;
    private float freezecount;
    public bool freeze = false;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;

        freeze = true;
        Debug.Log("item freezed");
        freezecount = freezetime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //adjust this function slightly when start to implement player and chest inventory
        var inventory = other.transform.GetComponent<InventoryHolder>();
        
        if (!inventory || (freeze)) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
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
            Debug.Log("item unfreezed");
        }
    }
}

