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

public class PlaceObject : MonoBehaviour
{
    public MouseItemData mouseItem;
    public Transform player;
    public float placingRadius = 5f;   // how far player can place objects
    public float droppingRadius = 1f;
    public float gridWidth = 0.6f;
    public float gridHeight = 0.6f;
    private Dictionary<string, GameObject> locationVSgameobjects;
    private int stage = 1;   // stage = 1: right click to place object; stage = 2: right click to rotate object (rotatable object only)
    private GameObject current;   // point to current game object
    public GameObject placingRadiusobj;   // display the range of placing objects (circle around player)
    private InventoryItemData itemdata;
    // hotbar item goes here

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

    private GameObject createGameObject(string name, Vector3 pos, string key)
    {
        // create gameobject with name, scale at position pos

        /*
        GameObject currentObject = new GameObject(name);

        // add class components here
        House housetest = currentObject.AddComponent(typeof(House)) as House;
        //itemPkup.pickUpRadius = 0.5f;

        currentObject.transform.localScale = scale;
        currentObject.transform.position = pos;
        locationVSgameobjects.Add(key, currentObject);

        return currentObject;
        */

        //GameObject currentObject = Instantiate(GameObject.Find(name));
        GameObject currentObject = new GameObject(name);
        SpriteRenderer rend = currentObject.AddComponent<SpriteRenderer>();
        rend.sprite = itemdata.Icon;
        if (itemdata.colliderType == "circle")
        {
            CircleCollider2D cc = currentObject.AddComponent<CircleCollider2D>();
            cc.radius = itemdata.circleColliderRadius;
        }
        else if (itemdata.colliderType == "box")
        {
            var bc = currentObject.AddComponent<BoxCollider2D>();
            bc.size = itemdata.boxColliderSize;
        }
        ItemPickUp itempk = currentObject.AddComponent(typeof(ItemPickUp)) as ItemPickUp;
        itempk.ItemData = itemdata;

        //GameObject currentObject = Instantiate(Resources.Load(name) as GameObject);


        currentObject.transform.position = pos;
        if (itemdata.placeable)
        {
            locationVSgameobjects.Add(key, currentObject);
        }
        

        return currentObject;
    }
    
    void Start()
    {
        locationVSgameobjects = new Dictionary<string, GameObject>();
        placingRadiusobj.transform.position = player.transform.position;
        placingRadiusobj.transform.localScale = new Vector3(placingRadius, placingRadius, 0f);
        var sprr = placingRadiusobj.GetComponent<SpriteRenderer>();
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
        if (Mouse.current.rightButton.wasPressedThisFrame && stage == 1)
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
                if (locationVSgameobjects.ContainsKey(key))
                {
                    Debug.Log("cannot place here, something already exists");
                    return;
                }
                else if (Vector3.Distance(mousepos, playerpos) <= placingRadius) // and placeable
                {
                    // placeable items
                    //createGameObject("House", new Vector3(1f, 1f, 1f), mousepos, key);
                    current = createGameObject(itemdata.DisplayName, mousepos, key);
                    if (true) // object is rotatable
                    {
                        stage = 2;
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
                    current = createGameObject(itemdata.DisplayName, droppos, key);
                    Debug.Log("dropped item");
                }
                mouseItem.ClearSlot();
            }
            
            
            
            
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && stage == 2)
        {
            current.transform.Rotate(0f, 0f, 90f);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && stage == 1)
        {
            Vector3 playerpos = player.transform.position;
            for (int i = 0; i < mouseItem.AssignedInventorySlot.StackSize; i++)
            {
                Vector3 droppos = generateRandomPosition(playerpos);
                current = createGameObject(itemdata.DisplayName, droppos, "");
                Debug.Log("dropped item");
            }
            mouseItem.ClearSlot();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && stage == 2)
        {
            stage = 1;
        }
    }


}
