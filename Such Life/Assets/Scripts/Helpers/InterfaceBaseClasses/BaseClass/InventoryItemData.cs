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
/// <summary>
/// Class <c>Inventory Item Data</c> the most bare bones piece of data for the inventory system.
/// Relationship status : 
/// <c>ScriptableObject</c> based class
/// <c>InventorySlot</c> what it mainly uses it.
/// Is a Scriptable object so it can be made in the editor to represent an item
/// </summary>
public class InventoryItemData : ScriptableObject
{
    public int ID; //Also is all this stuff being public really good practice :thinky:
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public string Type;
    public int GoldValue; //For the shop

    public bool placeable;
    public bool rotatable; //is the placable object able to be rotated
    public GameObject placeObject;

    public string colliderType = "box"; // "box" refers to box collider, "circle" refers to circle collider, initizlized to box
    public Vector2 boxColliderSize = new Vector2(2f, 2f); // box collider initialized to 2x2
    public float circleColliderRadius;

    public GameObject ItemPrefab;

}
