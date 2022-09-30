using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlaceObject : MonoBehaviour
{
    public InventoryItemData inventoryItem;
    public Transform player;
    public float placingRadius = 5f;   // how far player can place objects
    public float gridWidth = 0.6f;
    public float gridHeight = 0.6f;
    private Dictionary<string, GameObject> locationVSgameobjects;
    // hotbar item goes here

    private Vector3 convertToGridCoordinate(Vector3 pos)
    {
        return new Vector3(gridWidth * (float)Math.Floor(pos.x / gridWidth)
            , gridHeight * (float)Math.Floor(pos.y / gridHeight), pos.z);
    }

    private void createGameObject(string name, Vector3 scale, Vector3 pos, string key)
    {
        // create gameobject with name, scale at position pos
        GameObject currentObject = new GameObject(name);

        // add class components here
        House housetest = currentObject.AddComponent(typeof(House)) as House;
        //itemPkup.pickUpRadius = 0.5f;

        currentObject.transform.localScale = scale;
        currentObject.transform.position = pos;
        locationVSgameobjects.Add(key, currentObject);
    }

    void Start()
    {
        locationVSgameobjects = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // create a gameobject for current hotbar item, place it on cursor's location pos
        // assuming hotbar item has a datatype InventoryItemData
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mousepos = Mouse.current.position.ReadValue();
            mousepos.z = Camera.main.nearClipPlane;

            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            mousepos = convertToGridCoordinate(mousepos);
            Debug.Log(mousepos);
            Vector3 playerpos = player.transform.position;
            mousepos.z = playerpos.z;
            string key = mousepos.x.ToString("0.00") + mousepos.y.ToString("0.00");
            if (true) // placeable
            {
                if (locationVSgameobjects.ContainsKey(key))
                {
                    Debug.Log("cannot place here, something already exists");
                    return;
                }
            }

            if (Vector3.Distance(mousepos, playerpos) <= placingRadius) // and placeable
            {
                // placeable items
                createGameObject("House", new Vector3(1f, 1f, 1f), mousepos, key);
                Debug.Log("placed " + "House");
            }
            else
            {
                if (true) // placeable
                {
                    // placeable item
                    Debug.Log("too far");
                }
                else // !placeable
                {
                    createGameObject("House", new Vector3(1f, 1f, 1f), mousepos, key);
                    Debug.Log("placed " + "House");
                }
            }
            
        }
    }


}
