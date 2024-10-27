using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Inventory System/Block Place Dictionairy")]
public class BlockItemDictionary : ScriptableObject
{
    public SerializableDictionary<TileBase, GameObject> tileToItemPrefabDict;
}
