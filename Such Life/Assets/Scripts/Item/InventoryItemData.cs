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
}
