using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public InventoryItemData ItemData;

    private CircleCollider2D myCollider;
    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        id = GetComponent<UniqueID>().ID;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);
        SaveLoad.onLoadGame += LoadGame;
        
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(id))
        {
            SaveGameManager.data.activeItems.Remove(id);
        }
        SaveLoad.onLoadGame -= LoadGame;
    }

    private void Start()
    {
        SaveGameManager.data.activeItems.Add(id, itemSaveData);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //adjust this function slightly when start to implement player and chest inventory
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        
        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }
}


[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(InventoryItemData _data, Vector3 _positon, Quaternion _rotation)
    {
        ItemData = _data;
        Position = _positon;
        Rotation = _rotation;
    }
}
