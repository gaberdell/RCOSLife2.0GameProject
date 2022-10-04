using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BuildingBase : MonoBehaviour
{
    // base class for all buildings

    // basic info
    public string name;
    public string tag;
    public int ID;
    public Vector2 position;
    
    // sprite info
    public Sprite display;

    // 2d collider info
    public string colliderType = "box"; // "box" refers to box collider, "circle" refers to circle collider, initizlized to box
    public Vector2 boxColliderSize = new Vector2(2f, 2f); // box collider initialized to 2x2
    public float circleColliderRadius;

    private int stage = 1; // current stage of placing this object. '1': rotate the object. '2': this building is being constructed by citizen. '3': this building is complete
                           // initialized to 1
    public bool rotatable = true; // check if this building object can be rotated, set to true initially
    public int buildingTime; // time needed to be built, in seconds
    private bool startBuilding = false; // whether this building is being built
    private float timer = 0f; 
        
    void rotateLeft() // rotate this building left by 90 degrees
    {
        if (rotatable && stage == 1)
        {
            transform.Rotate(0f, 0f, 90f);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame) // right click to rotate the building
        {
            rotateLeft();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame) // left click to start building
        {
            stage = 2;
            startBuilding = true;
        }
        if (startBuilding) // now the building is being built
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            if (seconds >= buildingTime)
            {
                stage = 3;
                startBuilding = false;
            }
        }
    }
}
