using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    public Vector3 mousePosition;
    public float cursorSpeed = 5f;
    public Rigidbody2D mouseBody;
    public Vector2 pos = new Vector2(0,0);

    // Update is called once per frame
    void Start() {
        mouseBody = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        pos = Vector2.Lerp(transform.position, mousePosition, cursorSpeed);
    }

    void FixedUpdate() { 
    
        mouseBody.MovePosition(mousePosition);



    }
}
