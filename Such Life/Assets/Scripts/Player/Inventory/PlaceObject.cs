/*
 * Placeable objects:
 * Right click to place item at cursor position (within player placing range: circle centered at player position with radius initialized to 5)
 * , place item based on grid (grid cell size initialized to 0.6x0.6).
 * Instruction: First left click on inventory slot item, then right click on tile map to place item on current grid cell, then right click to rotate 
 * item, finally left click to finish placing
 * 
 * Unplaceable objects:
 * Right click to drop item to a random position around player (within a drop range of radius initialized to 1, centered at player position)
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


/// <summary>
/// NOTE : THIS SCRIPT CURRENTLY GOES UNUSED AND IS PROBABLY BROKEN,
/// Class <c>Place Object</c> job is to place objects the player is holding.
/// Relationship status : 
/// <c>MooBehaviour</c> based class
/// <c>Mouse Item</c> what it mainly iterfaces with.
/// Removes the item it *has* in its inventory to place it
/// </summary>
public class PlaceObject : MonoBehaviour
{
    //WHY IS IT ALWAYS PUBLIC FUNCTIONS C# HAS BUILT-IN GETTERS AND SETTERS YOU KNOW
    [SerializeField]
    private MouseItemData mouseItem;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float placingRadius = 5f;   // how far player can place objects
    [SerializeField]
    private float droppingRadius = 1f;

    [SerializeField]
    public float gridWidth = 0.6f;
    [SerializeField]
    public float gridHeight = 0.6f;

    private bool isRotating = false;   // stage = 1: right click to place object; stage = 2: right click to rotate object (rotatable object only)
    private GameObject current;   // point to current game object
    public GameObject placingRadiusobj;   // display the range of placing objects (circle around player)
    private InventoryItemData itemdata;
    // hotbar item goes here

    private void OnEnable()
    {
        //CREATE A FUNCTION FOR WHEN ITS ABOUT TO BE PLACED
    }

    private void OnDisable()
    {
        //Memory removal for the same thing
    }


    private Vector3 convertToGridCoordinate(Vector3 pos)
    {
        return new Vector3(gridWidth * (float)Math.Floor(pos.x / gridWidth)
            , gridHeight * (float)Math.Floor(pos.y / gridHeight), pos.z);
    }

    private Vector3 generateRandomPosition(Vector3 playerPos)
    {
        // generate random position around player's position, within a circle of radius droppingRadius
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        float radius = (float)(rand.NextDouble() * droppingRadius);
        float tmp = (float)rand.NextDouble();
        tmp = 2 * tmp * radius - radius;
        float x = playerPos.x + tmp;
        float y;
        
        if (rand.NextDouble() < 0.5)
        {
            y = -(float)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(tmp, 2)) + playerPos.y;
        }
        else y = (float)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(tmp, 2)) + playerPos.y;
        return new Vector3(x, y, playerPos.z);
    }

    private GameObject createGameObject(GameObject prefab, Vector3 pos)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = pos;   

        return newObject;
    }
    
    void Start()
    {
        placingRadiusobj.transform.position = player.transform.position;
        placingRadiusobj.transform.localScale = new Vector3(placingRadius, placingRadius, 0f);
        SpriteRenderer sprr = placingRadiusobj.GetComponent<SpriteRenderer>();
        Color c = Color.white;
        c.a = 0f;
        sprr.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        // create a gameobject for current hotbar item, place it on cursor's location pos
        // assuming hotbar item has a datatype InventoryItemData
        itemdata = mouseItem.AssignedInventorySlot.ItemData;
        placingRadiusobj.transform.position = player.transform.position;
        if (itemdata && itemdata.placeable)
        {
            // draw circle (indicating placing radius) around player
            var sprr = placingRadiusobj.GetComponent<SpriteRenderer>();
            Color c = Color.white;
            c.a = 0.4f;
            sprr.color = c;
        }
        else
        {
            var sprr = placingRadiusobj.GetComponent<SpriteRenderer>();
            Color c = Color.white;
            c.a = 0f;
            sprr.color = c;
        }
        if (Mouse.current.rightButton.wasPressedThisFrame && isRotating == false)
        {
            Vector3 mousepos = Mouse.current.position.ReadValue();
            mousepos.z = Camera.main.nearClipPlane;

            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            mousepos = convertToGridCoordinate(mousepos);
            Vector3 playerpos = player.transform.position;
            mousepos.z = playerpos.z;
            string key = mousepos.x.ToString("0.00") + mousepos.y.ToString("0.00");

            if (itemdata.placeable) // placeable
            {
                //FEATURE TO REIMPLEMENT -> Check if place location has a collider if so don't place there
                /*if (locationVSgameobjects.ContainsKey(key))
                {
                    Debug.Log("cannot place here, something already exists");
                    return;
                }*/
                if (Vector3.Distance(mousepos, playerpos) <= placingRadius) // and placeable
                {
                    // placeable items
                    //current = createGameObject(itemdata.placeObject, mousepos);
                    if (itemdata.rotatable) // object is rotatable
                    {
                        isRotating = true;
                    }
                    Debug.Log("placed item");
                    mouseItem.ClearSlot();
                }
                else // out of placing range
                {
                    Debug.Log("too far");
                }
            }

            else // not placeable, drop item to a random position within a circle range
            {
                for (int i = 0; i < mouseItem.AssignedInventorySlot.StackSize; i++)
                {
                    Vector3 droppos = generateRandomPosition(playerpos);
                    current = createGameObject(itemdata.ItemPrefab, droppos);
                    Debug.Log("dropped item");
                }
                mouseItem.ClearSlot();
            }
            
            
            
            
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && isRotating == true)
        {
            current.transform.Rotate(0f, 0f, 90f);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && isRotating == false)
        {
            Vector3 playerpos = player.transform.position;
            for (int i = 0; i < mouseItem.AssignedInventorySlot.StackSize; i++)
            {
                //Swap out with a normalized vector from the players position to the drop point and multipled by the place distance vector
                Vector3 droppos = generateRandomPosition(playerpos);
                //current = createGameObject(itemdata.placeObject, droppos);
                Debug.Log("dropped item");
            }
            mouseItem.ClearSlot();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && isRotating == true)
        {
            isRotating = false;
        }
    }


}
