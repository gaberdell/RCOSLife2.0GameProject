using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/* Description:  scriptable object that defines what an item is in the game.
 * Can be inherited from and to have variants of the items (placeable objects, potions, tools,...).
 * Can be adjust and add in more values.
 * Can derive new item.
 */

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public string Type;
    public int GoldValue; //For the shop

    public bool placeable;

    public string colliderType = "box"; // "box" refers to box collider, "circle" refers to circle collider, initizlized to box
    public Vector2 boxColliderSize = new Vector2(2f, 2f); // box collider initialized to 2x2
    public float circleColliderRadius;

    public GameObject ItemPrefab;

}

