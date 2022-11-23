using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MouseFollow
{
    public Rigidbody2D body;
    public Animator anim;
    public Transform interactor; // Shows what user inputted
    public float walkSpeed;
    public Vector2 direction;

    private float inputX;
    private float inputY;

    public float teleportDistance;

    public playerAction playerControls;
    private InputAction teleport;
    private InputAction dash;
    public float teleportCooldown; //how long between you can use consecutive teleports (seconds)
    public float teleportDelay; //there is a delay between after the teleport and when you can move (seconds)
    public float teleportTime; //Time for last used teleport
    public bool canMove; //whether or not you can move, ex if you are stunned or after you tp
    public float dashTime; //how long dash lasts
    public float dashSpeed; //how fast the character is when he is dashing
    public bool canDash; 
    public bool combatMove; //if the player is currently dashing/rolling/ability related to combat movement, it should not be able to move until it is finished


    void Awake()
    {
        walkSpeed = 2;
        dashSpeed = 5;
        playerControls = new playerAction();
        teleportDistance = 3;
        teleportCooldown = 8;
        teleportTime = 0 - teleportCooldown; //allow you to use teleport as soon as you spawn in
        teleportDelay = .5f;
        canMove = true;
        canDash = true;
        combatMove = false; 
        dashTime = .2f;
    }
    // Update is called once per frame
    void Update() {
        if (canMove == true)
        {
            direction = playerControls.Player.Move.ReadValue<Vector2>();
            inputX = direction.x;
            inputY = direction.y;
        }
        else
        {
            inputX = 0;
            inputY = 0;
        }

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
            if (inputY < 0) {//If character is walking southwards while facing north, character is walking backwards.
               anim.SetBool("isFacingForward", false); // If walking south, character is NOT facing forward.
            }
            else {
               anim.SetBool("isFacingForward", true); //Else, character is facing forward.
            }
        }
        if (mousePosition.x - body.position.x > 0.5 && mousePosition.y - body.position.y > 0.5) /*NE*/ {
            if (inputX < 0 && inputY < 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > 0 && mousePosition.y - body.position.y > -0.5 && mousePosition.y - body.position.y < 0.5) /*E*/ {
            if (inputX < 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > 0.5 && mousePosition.y - body.position.y < -0.5) /*SE*/ {
            if (inputX < 0 && inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x > -0.5 && mousePosition.x - body.position.x < 0.5 && mousePosition.y - body.position.y < 0) /*S*/ {
            if (inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < -0.5 && mousePosition.y - body.position.y < -0.5) /*SW*/ {
            if (inputX > 0 && inputY > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < 0 && mousePosition.y - body.position.y > -0.5 && mousePosition.y - body.position.y < 0.5) /*W*/ {
            if (inputX > 0) {
               anim.SetBool("isFacingForward", false); 
            }
            else {
               anim.SetBool("isFacingForward", true); 
            }
        }
        if (mousePosition.x - body.position.x < -0.5 && mousePosition.y - body.position.y > 0.5) /*NW*/ {
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


        if (canMove == false && Time.time > teleportTime + teleportDelay) /*allows the player to move again after teleporting*/
        {
            canMove = true;
        }
    }


        
    void FixedUpdate() {
        if (combatMove == false)
        {
            if (canMove == true)
            {
                body.velocity = new Vector2(direction.x * walkSpeed, direction.y * walkSpeed);
            }
            else
            {
                body.velocity = new Vector2();
            }
        }
    }
    
    private void OnEnable(){
        playerControls.Player.Enable();
        teleport = playerControls.Player.Teleport;
        teleport.Enable();
        teleport.performed += TeleportFunct;
        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += DashFunct;
    }
    private void OnDisable(){
        playerControls.Player.Disable();
        teleport.Disable();
        dash.Disable();

    }

    //combat movement functions


    //this function teleports the player in the direction that they're moving. Currently binded to T
    //after it is called there is a cooldown period when it cannot be used again and a delay where you cannot move after using it.
    //currently does not check whether or not you are allowed to land at the spot where you teleport
    private void TeleportFunct(InputAction.CallbackContext context)
    {
        if (Time.time > teleportTime + teleportCooldown && canMove == true)
        {
            teleportTime = Time.time;
            body.position = teleportDistance * direction + body.position;
            Debug.Log("called teleport function");
            canMove = false;
        }
        else
        {
            float timeRemaining = teleportCooldown - (Time.time - teleportTime);
            Debug.Log("Cannot teleport: teleport is on cooldown. " + timeRemaining.ToString("F2") + "seconds left");
        }
    }

    private void DashFunct(InputAction.CallbackContext context)
    {
        StartCoroutine(DashRoutine());
    }

    //coroutine which basically buffs the speed for a short time to dash
    //currently binded to space
    private IEnumerator DashRoutine()
    {
        Vector2 currentDirection = direction;
        canDash = false;
        combatMove = true;
        Debug.Log("Called dash routine");
        float StartDashTime = Time.time;
        body.velocity = new Vector2(currentDirection.x * dashSpeed, currentDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashTime);
        combatMove = false;
        yield return null;
    }
}