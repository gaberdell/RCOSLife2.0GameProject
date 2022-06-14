using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    //public SpriteRenderer spriteRenderer;



    public float walkSpeed;
    public float frameRate;

    Vector2 direction;

    /*
    void SpriteFlip() {
        //if we are facing right, and the player holds left, flip.
        if(!spriteRenderer.flipX && direction.x < 0){
            spriteRenderer.flipX = true;
        } else if (spriteRenderer.flipX && direction.x > 0) /*if we are facing left, and the player holds right, flip.
        {
            spriteRenderer.flipX = false;
        }
    }
    */

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //character movement
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
    }

    void FixedUpdate() {
        //Movement
        //SpriteFlip();
        body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);
    }
}
