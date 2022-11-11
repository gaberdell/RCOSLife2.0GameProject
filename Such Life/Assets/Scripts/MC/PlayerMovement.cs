using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MouseFollow
{
    public Rigidbody2D body;
    public Animator anim;
    public Transform interactor; // Shows mouse position
    public Transform interactor_two; // Shows what user inputted
    public float walkSpeed;
    public Vector2 direction;

    private float inputX;
    private float inputY;

    public float teleportDistance;

    public playerAction playerControls;
    private InputAction teleport;


    void Awake()
    {
        playerControls = new playerAction();
        teleportDistance = 3;
        
    }
    // Update is called once per frame
    void Update() {

        direction = playerControls.Player.Move.ReadValue<Vector2>();
        inputX = direction.x;
        inputY = direction.y;

        direction = new Vector2(inputX, inputY).normalized;

        anim.SetFloat("Horizontal", inputX);
        anim.SetFloat("Vertical", inputY);
        anim.SetFloat("Speed", direction.sqrMagnitude);

        
        //give the game info of the direction that the player is facing (for interaction feature later)
    
        if(Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) {
            anim.SetFloat("LastHorizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("LastVertical", Input.GetAxis("Vertical"));
        }

     
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);



        //Will now turn interactor towards cardinal direction of mouse.
        if (mousePosition.x - body.position.x > -0.5 && mousePosition.x - body.position.x < 0.5 && mousePosition.y - body.position.y > 0) /*N*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, 180);
            if (inputY < 0) {//If character is walking southwards/opposite of its intended direction while facing north/its intended direction, 
                             //character is walking backwards.
               anim.SetBool("isFacingForward", false); // If walking south, character is NOT facing forward/its intended direction.
            }
            else {
               anim.SetBool("isFacingForward", true); //Else, character is facing forward/its intended direction.
            }
        }
        if (mousePosition.x - body.position.x > 0.5 && mousePosition.y - body.position.y > 0.5) /*NE*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, 135);
            if (inputX < 0 && inputY < 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > 0 && mousePosition.y - body.position.y > -0.5 && mousePosition.y - body.position.y < 0.5) /*E*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, 90);
            if (inputX < 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > 0.5 && mousePosition.y - body.position.y < -0.5) /*SE*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, 45);
            if (inputX < 0 && inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > -0.5 && mousePosition.x - body.position.x < 0.5 && mousePosition.y - body.position.y < 0) /*S*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, 0);
            if (inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < -0.5 && mousePosition.y - body.position.y < -0.5) /*SW*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, -45);
            if (inputX > 0 && inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < 0 && mousePosition.y - body.position.y > -0.5 && mousePosition.y - body.position.y < 0.5) /*W*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, -90);
            if (inputX > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < -0.5 && mousePosition.y - body.position.y > 0.5) /*NW*/ {
            interactor_two.localRotation = Quaternion.Euler(0, 0, -135);
            if (inputX > 0 && inputY < 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        anim.SetFloat("MouseX", mousePosition.x - body.position.x);
        anim.SetFloat("MouseY", mousePosition.y - body.position.y);

        
        
        
        

        //Tracks what user is inputting.
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

    
        
    void FixedUpdate() {  
        body.velocity = new Vector2(direction.x * walkSpeed, direction.y * walkSpeed);
    }
    
    private void OnEnable(){
        playerControls.Player.Enable();
        teleport = playerControls.Player.Teleport;
        teleport.Enable();
        teleport.performed += TeleportFunct;
    }
    private void OnDisable(){
        playerControls.Player.Disable();
        teleport.Disable();

    }

    //combat movement functions

    private void TeleportFunct(InputAction.CallbackContext context)
    {
        body.position = teleportDistance * direction + body.position;
        Debug.Log("called teleport function");
    }
    
}