using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public Transform interactor;
    public float walkSpeed;
    Vector2 direction;

    // Update is called once per frame
    void Update() {
        //character movement
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("Speed", direction.sqrMagnitude);



        //remember the last horizontal and vertical float to set the idle animation correctly
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            anim.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }


        //give the game info of the direction that the player is facing (for interaction feature later)
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") > 0) /*N*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0) /*NE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 135);
        } 
        if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0) /*E*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
        }   
        if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0) /*SE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 45);
        }
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0) /*S*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0) /*SW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -45);
        }
        if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0) /*W*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0) /*NW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -135);
        }


    }

    void FixedUpdate() {
        //Movement
        body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);
    }
}
