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
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("Speed", direction.sqrMagnitude);



        //remember the last horizontal and vertical float to set the idle animation correctly
        if(Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) {
            anim.SetFloat("LastHorizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("LastVertical", Input.GetAxis("Vertical"));
        }


        //give the game info of the direction that the player is facing (for interaction feature later)
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0) /*N*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) /*NE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 135);
        } 
        if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0) /*E*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
        }   
        if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) /*SE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 45);
        }
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0) /*S*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) /*SW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -45);
        }
        if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0) /*W*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0) /*NW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -135);
        }


    }

    void FixedUpdate() {
        //Movement
        body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);
    }
}
