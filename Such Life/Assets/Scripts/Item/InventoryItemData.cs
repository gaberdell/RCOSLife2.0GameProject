using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */

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

    public bool placeable;

    public string colliderType = "box"; // "box" refers to box collider, "circle" refers to circle collider, initizlized to box
    public Vector2 boxColliderSize = new Vector2(2f, 2f); // box collider initialized to 2x2
    public float circleColliderRadius;
    
}
