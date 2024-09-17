using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Inventory System/Block Place Data")]
public class BlockPlaceData : ScriptableObject
{
    public GameObject placeGameObject;
    public Tile placeTile;
}
