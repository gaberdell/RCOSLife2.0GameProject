using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slim : mobBase
{

    public Transform player;
    public float awareness;
    public float radius;
    public float angle;
    //public UnityEngine.AI.NavMeshAgent player;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        currPosition = new Vector2(transform.position.x, transform.position.y);
        playerSighted = false;
    }

    void Update(){

        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            PositionChange();
        }

        monster.velocity = transform.up/2;
    }

    void PositionChange()
    {

        if(playerSighted){
            chasing();
        }
        else{
            
            wandering();
        }
        
    }

    void chasing(){

    }

    void wandering(){
        //the character will walk in random angles for every 1.3s
        float angle = Random.Range(0f, 360f);
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward); //rotate the character with random angle
        Vector3 latestUP = rotate* Vector3.up;
        latestUP.z = 0;
        latestUP.Normalize();
        transform.up = latestUP;
        timeToChangeDirection = 2.0f;
    }

    //Checking on the FOV circle in an interval
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            //FOV();
        }
    }

    //check for collider on our target layer
    /*private void FOV()
    {
        Collider2D[] rangeCheckPlayer = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerPlayer);
        //Check to see if anything in the collison box
        if (rangeCheckPlayer.Length > 0)
        {
            //check to see if the player is detected within range
            Transform targetPlayer = rangeCheckPlayer[0].transform;
            Vector2 directionToTargetPlayer = (targetPlayer.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTargetPlayer) < angle / 2)
            {
                float distanceToTargetPlayer = Vector2.Distance(transform.position, targetPlayer.position);
                //check to see if the Raycast hit the obstructionLayer object or not, if not
                //the enemy spotted the player
                if (!Physics2D.Raycast(transform.position, directionToTargetPlayer, distanceToTargetPlayer, obstructionLayer))
                {
                    playerSighted = true;
                }
                else
                {
                    playerSighted = false;
                }
            }
            else
            {
                playerSighted = false;
            }
        }
        else if (playerSighted)
        {
            playerSighted = false;
        }
    }*/

}
