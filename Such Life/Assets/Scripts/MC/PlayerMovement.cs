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
    public playerAction playerControls;
    public Vector2 direction;


    private float inputX;
    private float inputY;
    void Awake(){
        playerControls = new playerAction();
        direction = new Vector2();
    }
    // Update is called once per frame
    void Update() {
        playerControls.Player.Enable();
        direction = playerControls.Player.Move.ReadValue<Vector2>();
        inputX = direction.x;
        inputY = direction.y;
        //playerControls.
        //    inputX = Input.GetAxis("Horizontal");
        //    inputY = Input.GetAxis("Vertical");

        //     direction = new Vector2(inputX, inputY).normalized;
        anim.SetFloat("Horizontal", inputX);
        anim.SetFloat("Vertical", inputY);
        anim.SetFloat("Speed", direction.sqrMagnitude);

 //       give the game info of the direction that the player is facing (for interaction feature later)
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

    private void OnMove(InputValue value){
         direction = value.Get<Vector2>();   
    }

    void FixedUpdate() {
    
        body.velocity = new Vector2(direction.x * walkSpeed, direction.y * walkSpeed);



    }

    private void onEnable(){
        playerControls.Enable();
    }
    private void onDisable(){
        playerControls.Disable();

    }
}