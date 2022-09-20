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

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();
        
        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
