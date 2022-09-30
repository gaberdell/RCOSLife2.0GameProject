using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public Transform interactor;
    public float walkSpeed;
    Vector2 direction;


    private float inputX;
    private float inputY;

    // Update is called once per frame
    void Update() {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        direction = new Vector2(inputX, inputY).normalized;

        anim.SetFloat("Horizontal", inputX);
        anim.SetFloat("Vertical", inputY);
        anim.SetFloat("Speed", direction.sqrMagnitude);
    }

    void FixedUpdate() {
    
        body.velocity = new Vector2(direction.x * walkSpeed, direction.y * walkSpeed);



    }
}

