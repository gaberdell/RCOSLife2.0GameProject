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
        //character movement
        direction = new Vector2(inputX, inputY).normalized;
        anim.SetFloat("Horizontal", inputX);
        anim.SetFloat("Vertical", inputY);
        anim.SetFloat("Speed", direction.sqrMagnitude);



        //remember the last horizontal and vertical float to set the idle animation correctly
        if (inputX == 1 || inputX == -1 || inputY == 1 || inputY == -1) {
            anim.SetFloat("LastHorizontal", inputX);
            anim.SetFloat("LastVertical", inputY);
        }


        //give the game info of the direction that the player is facing (for interaction feature later)
        if (inputX == 0 && inputY > 0) /*N*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (inputX > 0 && inputY > 0) /*NE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 135);
        }
        if (inputX > 0 && inputY == 0) /*E*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (inputX > 0 && inputY < 0) /*SE*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 45);
        }
        if (inputX == 0 && inputY < 0) /*S*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (inputX < 0 && inputY < 0) /*SW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -45);
        }
        if (inputX < 0 && inputY == 0) /*W*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (inputX < 0 && inputY > 0) /*NW*/ {
            interactor.localRotation = Quaternion.Euler(0, 0, -135);
        }
    }

    //get whatever input it being send in by the InputSystem
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }


    void FixedUpdate() {
        //Movement
        //body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);

        body.velocity = new Vector2(inputX * walkSpeed, inputY * walkSpeed);



    }
}
